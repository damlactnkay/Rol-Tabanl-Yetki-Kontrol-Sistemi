using Application.Interfaces.Ozel;
using Entity.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication45.Controllers
{
    public class BolumController : Controller
    {
        private readonly IBolumRepository _bolumRepo;

        public BolumController(IBolumRepository bolumRepo)
        {
            _bolumRepo = bolumRepo;
        }

        public async Task<IActionResult> Index()
        {
            var bolumler = await _bolumRepo.GetAllAsync();
            ViewBag.Bolumler = new SelectList(bolumler, "Id", "BolumAdi");
            return View();
        }

       
        [HttpGet]
        public async Task<JsonResult> GetBolum(int id)
        {
            var bolum = await _bolumRepo.GetByIdAsync(id);
            if (bolum == null)
                return Json(null);

            return Json(new { bolum.Id, bolum.BolumAdi });
        }
    }
}
