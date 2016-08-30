using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoMarathon.Data.Entities;
using PhotoMarathon.Service.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoMarathon.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGeneralService generalService;
        public AccountController(IGeneralService generalService)
        {
            this.generalService = generalService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            var photographer = new Photographer();
            ViewBag.WorkShops = generalService.GetWorkShpos().Data;
            return View(photographer);
        }
        [HttpPost]
        public IActionResult Register(Photographer photographer)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.WorkShops = generalService.GetWorkShpos().Data;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
