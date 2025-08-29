using Application.Interfaces.Ozel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication45.ViewComponents
{
    public class CascadingDropdownViewComponent : ViewComponent
    {
        
            private readonly IBolumRepository _bolumRepo;

            public CascadingDropdownViewComponent(IBolumRepository bolumRepo)
            {
                _bolumRepo = bolumRepo;
            }

            public async Task<IViewComponentResult> InvokeAsync()
            {
                var bolumler = await _bolumRepo.GetAllAsync();
                ViewBag.Bolumler = new SelectList(bolumler, "Id", "BolumAdi");
                return View(); // Views/Shared/Components/CascadingDropdownList/Default.cshtml
            }
        }
    }
