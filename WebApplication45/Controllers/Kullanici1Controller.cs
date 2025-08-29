using Application.Interfaces.Ozel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication45.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public class Kullanici1Controller : Controller
    {
        private readonly IProductRepository _productRepo;

        public Kullanici1Controller(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        // Listeleme
        public async Task<IActionResult> Index()
        {
            var products = await _productRepo.GetAllAsync();
            return View(products);
        }

    }
}
