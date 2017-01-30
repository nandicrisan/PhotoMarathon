using Microsoft.AspNetCore.Mvc;
using PhotoMarathon.Data.Entities;

namespace PhotoMarathon.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
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
