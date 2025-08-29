using Application.Interfaces.Ozel;
using Entity.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection.PortableExecutable;

namespace WebApplication45.Controllers
{
    [Route("Admin/[controller]")]
    public class MakineController : Controller
    {
        private readonly IBolumRepository _bolumRepo;
        private readonly IMakineRepository _makineRepo;

        public MakineController(IBolumRepository bolumRepo, IMakineRepository makineRepo)
        {
            _bolumRepo = bolumRepo;
            _makineRepo = makineRepo;
        }

        // Sayfa ilk açıldığında
        public async Task<IActionResult> Index()
        {
            var bolumler = await _bolumRepo.GetAllAsync();
            ViewBag.Bolumler = new SelectList(bolumler, "Id", "BolumAdi");
            return View();
        }

        // AJAX çağrısı ile makineleri getirir
        [HttpGet("GetMakineler")]
        public async Task<JsonResult> GetMakineler(int bolumId)
        {
            if (bolumId <= 0)
                return Json(new List<Makineler>());

            var makineler = await _makineRepo.GetByBolumIdAsync(bolumId);
            return Json(makineler.Select(m => new { m.Id, m.MakineAdi }));

        }
        [HttpGet]
        public async Task<IActionResult> GetMakinelerByBolum(int bolumId)
        {
            var makineler = await _makineRepo.GetByBolumIdAsync(bolumId);

            // 2. Eğer veri yoksa boş liste dön
            if (makineler == null || !makineler.Any())
                return Json(new List<object>());

            // 3. JS’in beklediği formatta JSON dön
            var result = makineler.Select(m => new { id = m.Id, ad = m.MakineAdi });
            return Json(result);
        }

    }
}