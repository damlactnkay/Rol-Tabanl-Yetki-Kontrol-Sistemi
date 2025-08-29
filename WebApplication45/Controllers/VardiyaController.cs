using Application.Interfaces.Ozel;
using Entity.Class;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication45.Controllers
{
    [Route("Admin/[controller]")]
    public class VardiyaController : Controller
    {
        private readonly IVardiyaRepository _vardiyaRepo;

        public VardiyaController(IVardiyaRepository vardiyaRepo)
        {
            _vardiyaRepo = vardiyaRepo;
        }

        public async Task<IActionResult> Index()
        {
            var vardiyalar = await _vardiyaRepo.GetAllAsync();
            return View(vardiyalar);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vardiya vardiya)
        {
            await _vardiyaRepo.AddAsync(vardiya);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vardiya = await _vardiyaRepo.GetByIdAsync(id);
            return View(vardiya);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Vardiya vardiya)
        {
            await _vardiyaRepo.UpdateAsync(vardiya);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _vardiyaRepo.DeleteAsync(id);
            return RedirectToAction("Index");
        }

      
        public async Task<IActionResult> SidebarVardiyaList()
        {
            var vardiyalar = await _vardiyaRepo.GetAllAsync() ?? new List<Vardiya>();
            return PartialView("_SidebarVardiyaList", vardiyalar);
        }

        [HttpGet("GetVardiyalar")]
        public async Task<JsonResult> GetVardiyalar(int makineId)
        {
            if (makineId <= 0)
                return Json(new List<Vardiya>());

            var vardiyalar = await _vardiyaRepo.GetByMakineIdAsync(makineId);
            return Json(vardiyalar.Select(v => new { id = v.Id, vardiyaAdi = v.Kod }));
        }
        [HttpGet]
        public async Task<IActionResult> GetVardiyalarByMakine(int makineId)
        {
            var vardiyalar = await _vardiyaRepo.GetByMakineIdAsync(makineId);
            if (vardiyalar == null) return Json(new List<object>());
            var result = vardiyalar.Select(v => new { id = v.Id, ad = v.Kod });
            return Json(result);
        }

    }
}
