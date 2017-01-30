using Microsoft.AspNetCore.Mvc;
using PhotoMarathon.Data.Entities;
using PhotoMarathon.Service.Services;


namespace PhotoMarathon.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService contactService;

        public ContactController(
            IContactService contactService)
        {
            this.contactService = contactService;
        }
        // GET: /<controller>/
        public IActionResult Message(ContactMessage model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var res = contactService.Add(model);
            if (!res.IsOk())
                return StatusCode(500, res.Message);
            TempData.Add("Message", "Messaj trimis");
            TempData.Add("ServerMessageType", "success");
            return RedirectToAction("Contact", "Home");
        }
    }
}
