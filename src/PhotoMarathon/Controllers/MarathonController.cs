using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoMarathon.Controllers
{
    public class MarathonController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Exposition()
        {
            return View();
        }

        public IActionResult Workshop()
        {
            return View();
        }

        public IActionResult Photographic()
        {
            return View();
        }
    }
}
