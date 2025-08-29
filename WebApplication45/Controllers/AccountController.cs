using Application.Interfaces.Ozel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApplication45.Controllers
{
   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepo;
        public AccountController(IUserRepository userRepo)
            => _userRepo = userRepo;
        [HttpGet]
         
        public IActionResult Login()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string KullaniciAdi, string Password)
        {
            if (string.IsNullOrEmpty(KullaniciAdi) || string.IsNullOrEmpty(Password))
            {
                TempData["LoginError"] = "Kullanıcı adı veya şifre boş olamaz.";
                return View();
            }

            var user = await _userRepo.GetByUsernameAsync(KullaniciAdi, Password);

            if (user == null)
            {
                TempData["LoginError"] = "Kullanıcı adı veya şifre hatalı!";
                return View();
            }

            // Claims oluştur
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.KullaniciAdi),
        new Claim(ClaimTypes.Role, user.Role),
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Role göre yönlendirme
            if (user.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                // Admin değilse tüm diğer kullanıcılar Kullanici1 sayfasına
                return RedirectToAction("Index", "Kullanici1");
            }
        }


        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Authorize]
        public IActionResult LogoutConfirm()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Logout()
        {
            
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

          
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";

          
            return RedirectToAction("Login", "Account");
        }

    }
}

