using Application.Interfaces.Ozel;
using Entity.Class;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.PortableExecutable;

namespace WebApplication45.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
   // [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
      
        private readonly IProductRepository _productRepo;
        private readonly IBolumRepository _bolumRepo;
        private readonly IMakineRepository _makineRepo;
        private readonly IVardiyaRepository _vardiyaRepo;

        public AdminController(IProductRepository productRepo,
                          IBolumRepository bolumRepo,
                          IMakineRepository makineRepo,
                          IVardiyaRepository vardiyaRepo)
        {
            _productRepo = productRepo ?? throw new Exception("_productRepo null!");
            _bolumRepo = bolumRepo ?? throw new Exception("_bolumRepo null!");
            _makineRepo = makineRepo ?? throw new Exception("_makineRepo null!");
            _vardiyaRepo = vardiyaRepo ?? throw new Exception("_vardiyaRepo null!");
        }

        [Authorize]
        public async Task<IActionResult> Index(int? vardiyaId)
        {
            var urunler = await _productRepo.GetAllAsync();
            var bolumler = await _bolumRepo.GetAllAsync();
            ViewBag.Bolumler = bolumler.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.BolumAdi
            }).ToList();
            return View(urunler);
        }

        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditProduct(Uruns product)
        {
            if (!ModelState.IsValid) return View(product);

            await _productRepo.UpdateAsync(product);

            TempData["BasariliMesaj"] = "Ürün başarıyla güncellendi!";

            return RedirectToAction(nameof(EditProduct), new { id = product.Id });
        }

      
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var urun = await _productRepo.GetByIdAsync(id);
            if (urun == null) return NotFound();

            return View(urun); 
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(Uruns product)
        {
            await _productRepo.DeleteHierarchyByBolumIdAsync(product.Id);
            return RedirectToAction("Index", "Admin");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> VardiyayaGoreUrunler(int vardiyaId)
        {
            ViewBag.isAdmin = User.IsInRole("Admin");
            var urunler = await _productRepo.GetByVardiyaIdAsync(vardiyaId);
            return PartialView("_UrunListPartial", urunler);
        }


        // 🔍 Ürün arama (PartialView döner)
        [HttpGet]
        public async Task<IActionResult> SearchProducts(string urunAdi)
        {
            if (string.IsNullOrWhiteSpace(urunAdi))
                return PartialView("_UrunListPartial", new List<Uruns>());
            ViewBag.isAdmin = User.IsInRole("Admin");
            // Repo içinde hazır bir Search yoksa LINQ ile filtreleme yapabilirsin:
            var allProducts = await _productRepo.GetAllAsync();
            var urunler = allProducts
                            .Where(u => u.UrunAdi.Contains(urunAdi, StringComparison.OrdinalIgnoreCase))
                            .ToList();
            return PartialView("_UrunListPartial", urunler);
        }

        // 📋 Tüm ürünleri listeleme (PartialView döner)
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var urunler = await _productRepo.GetAllAsync();
            ViewBag.isAdmin = User.IsInRole("Admin");
            return PartialView("_UrunListPartial", urunler);
        }
        [HttpGet]
        public async Task<IActionResult> GetMakinelerByBolum(int bolumId)
        {
            if (bolumId <= 0) return Json(new List<object>());

            var makineler = await _makineRepo.GetByBolumIdAsync(bolumId) ?? new List<Makineler>();

            var result = makineler.Select(m => new
            {
                id = m.Id,
                ad = m.MakineAdi
            }).ToList();

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetVardiyalarByMakine(int makineId)
        {
            if (makineId <= 0) return Json(new List<object>());

            var vardiyalar = await _vardiyaRepo.GetByMakineIdAsync(makineId) ?? new List<Vardiya>();

            var result = vardiyalar.Select(v => new
            {
                id = v.Id,
                kod = v.Kod
            }).ToList();

            return Json(result);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            // Bölümleri yükle
            var bolumler = await _bolumRepo.GetAllAsync() ?? new List<Bolum>();
            ViewBag.Bolumler = bolumler
                .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.BolumAdi })
                .ToList();

            // Makine ve vardiya dropdownları başlangıçta boş
            ViewBag.Makineler = new List<SelectListItem>
{
    new SelectListItem { Value = "", Text = "Önce bölüm seçiniz" }
};
            ViewBag.Vardiyalar = new List<SelectListItem>
{
    new SelectListItem { Value = "", Text = "Önce makine seçiniz" }
};

            return View(new UrunEkle());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UrunEkle model)
        {
            if (!ModelState.IsValid)
            {
                // Bölümleri yeniden yükle
                var bolumler = await _bolumRepo.GetAllAsync() ?? new List<Bolum>();
                ViewBag.Bolumler = bolumler
                    .Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.BolumAdi })
                    .ToList();

                // Seçilen bölüme göre makineleri yükle
                var makineler = model.BolumId > 0
                    ? await _makineRepo.GetByBolumIdAsync(model.BolumId) ?? new List<Makineler>()
                    : new List<Makineler>();

                ViewBag.Makineler = new List<SelectListItem>
    {
        new SelectListItem { Value = "", Text = model.BolumId > 0 ? "Seçiniz" : "Önce bölüm seçiniz" }
    };
                ViewBag.Makineler.AddRange(makineler
                    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.MakineAdi }));

                // Seçilen makineye göre vardiyaları yükle
                var vardiyalar = model.MakineId > 0
                    ? await _vardiyaRepo.GetByMakineIdAsync(model.MakineId) ?? new List<Vardiya>()
                    : new List<Vardiya>();

                ViewBag.Vardiyalar = new List<SelectListItem>
    {
        new SelectListItem { Value = "", Text = model.MakineId > 0 ? "Seçiniz" : "Önce makine seçiniz" }
    };
                ViewBag.Vardiyalar.AddRange(vardiyalar
                    .Select(v => new SelectListItem { Value = v.Id.ToString(), Text = v.Kod }));

                return View(model);
            }

            // Model valid ise kaydet
            var urun = new Uruns
            {
                UrunAdi = model.UrunAdi,
                UrunFiyat = decimal.TryParse(model.UrunFiyat, out var fiyat) ? fiyat : 0
            };

            await _productRepo.AddAsync(urun);

            TempData["BasariliMesaj"] = "Ürün başarıyla eklendi!";
            return RedirectToAction("Index");
        }
    }
}
 