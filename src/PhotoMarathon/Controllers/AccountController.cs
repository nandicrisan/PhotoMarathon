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
        private readonly IAccountService accountService;
        public AccountController(IGeneralService generalService, IAccountService accountService)
        {
            this.generalService = generalService;
            this.accountService = accountService;
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
            photographer.HasNewsLetter = true;
            ViewBag.WorkShops = generalService.GetWorkShpos().Data;
            return View(photographer);
        }
        [HttpPost]
        public IActionResult Register(Photographer photographer)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.WorkShops = generalService.GetWorkShpos().Data;
                return View(photographer);
            }
            if (!photographer.Rules)
            {
                ViewBag.WorkShops = generalService.GetWorkShpos().Data;
                ModelState.AddModelError("Rules", "Te rugăm să confirmi că ai citit şi eşti de acord cu regulamentul");
                return View(photographer);
            }
            var result = accountService.AddPhotographer(photographer);
            if (!result.IsOk())
            {
                ModelState.AddModelError("Rules", "Eroare! Te rugăm să încerci mai târziu.");
                ViewBag.WorkShops = generalService.GetWorkShpos().Data;
                return View(photographer);
            }
            ViewData["Message"] = "Te-ai înregistrat cu succes!";
            ViewData["ServerMessageType"] = "success";
            return RedirectToAction("Index", "Home");
        }
    }
}
