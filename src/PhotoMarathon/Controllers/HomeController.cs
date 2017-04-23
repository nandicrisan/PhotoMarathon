using Microsoft.AspNetCore.Mvc;
using PhotoMarathon.Data.Entities;
using PhotoMarathon.Service.Services;

namespace PhotoMarathon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICmsService _cmsService;
        public HomeController(ICmsService cmsService)
        {
            _cmsService = cmsService;
        }
        public IActionResult Index()
        {
            var page = _cmsService.GetPage("home");
            if (!page.IsOk())
                return new StatusCodeResult(404);
            return View(page.Data);
        }

        //slug:despre
        public IActionResult About()
        {
            var page = _cmsService.GetPage("despre");
            if(!page.IsOk())
                return new StatusCodeResult(404);
            return View(page.Data);
        }
    
        public IActionResult Partners()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View(new ContactMessage());
        }

        public IActionResult Rules()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
