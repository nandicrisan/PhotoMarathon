using Microsoft.AspNetCore.Mvc;
using PhotoMarathon.Service.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoMarathon.Controllers
{
    public class MarathonController : Controller
    {
        private readonly ICmsService _cmsService;
        public MarathonController(ICmsService cmsService)
        {
            _cmsService = cmsService;
        }
 
        public IActionResult Exposition()
        {
            var page = _cmsService.GetPage("expoziti");
            if (!page.IsOk())
                return new StatusCodeResult(404);
            return View(page.Data);
        }

        public IActionResult Workshop()
        {
            var page = _cmsService.GetPage("workshop");
            if (!page.IsOk())
                return new StatusCodeResult(404);
            return View(page.Data);
        }

        public IActionResult Photographic()
        {
            var page = _cmsService.GetPage("marathon-de-fotografie");
            if (!page.IsOk())
                return new StatusCodeResult(404);
            return View(page.Data);
        }
    }
}
