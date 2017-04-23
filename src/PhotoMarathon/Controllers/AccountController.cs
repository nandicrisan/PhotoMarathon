using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoMarathon.Data.Entities;
using PhotoMarathon.Models;
using PhotoMarathon.Service.Services;
using System;
using System.Threading.Tasks;

namespace PhotoMarathon.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGeneralService _generalService;
        private readonly IAccountService _accountService;
        private readonly INewsLetterService _newsLetterService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IGeneralService generalService,
            IAccountService accountService,
            UserManager<ApplicationUser> userManager,
            INewsLetterService newsLetterService,
            SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            this._generalService = generalService;
            this._accountService = accountService;
            this._newsLetterService = newsLetterService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            var viewModel = new RegisterViewModel();

            var photographer = new Photographer();
            photographer.HasNewsLetter = true;
            ViewBag.WorkShops = _generalService.GetWorkShpos().Data.GetRange(3, 1);
            viewModel.Photographer = photographer;

            var registerStatus = _generalService.GetRegisterStatus();

            viewModel.RegisterStatus = new RegisterStatus();
            if (registerStatus.IsOk())
                viewModel.RegisterStatus = registerStatus.Data;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Register(Photographer photographer)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.WorkShops = _generalService.GetWorkShpos().Data;
                return View(photographer);
            }
            if (!photographer.Rules)
            {
                ViewBag.WorkShops = _generalService.GetWorkShpos().Data;
                ModelState.AddModelError("Rules", "Te rugăm să confirmi că ai citit şi eşti de acord cu regulamentul");
                return View(photographer);
            }
            var result = _accountService.AddPhotographer(photographer);
            var newsLetter = new Newsletter
            {
                DateAdded = DateTime.Now,
                Email = photographer.Email,
                Name = photographer.FirstName + " " + photographer.LastName
            };
            var addNewsLetter = _newsLetterService.Add(newsLetter);
            if (!result.IsOk() || !result.IsOk())
            {
                ModelState.AddModelError("Rules", "Eroare! Te rugăm să încerci mai târziu.");
                ViewBag.WorkShops = _generalService.GetWorkShpos().Data;
                return View(photographer);
            }
            TempData.Add("Message", "Te-ai înregistrat cu succes!");
            TempData.Add("ServerMessageType", "success");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, };
                var signImRes = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                if (signImRes.Succeeded)
                    return RedirectToAction("Index", "Admin");
                else
                    ModelState.AddModelError("UserName", "Numele de utilizator sau parola au fost introduse greșit");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        public IActionResult RegisterStatus(RegisterStatus model)
        {
            if (ModelState.IsValid)
            {
                var result = _generalService.SetRegisterStatus(model);
                if (result.IsOk())
                    return RedirectToAction("Photographers", "Admin");

                TempData.Add("Message", result.Message);
                return RedirectToAction("Photographers", "Admin");
            }
            return RedirectToAction("Photographers", "Admin");
        }
    }
}
