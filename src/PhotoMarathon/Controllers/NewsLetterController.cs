using Microsoft.AspNetCore.Mvc;
using PhotoMarathon.Data.Entities;
using PhotoMarathon.Service.Services;

namespace PhotoMarathon.Controllers
{
    public class NewsLetterController : Controller
    {
        private readonly INewsLetterService newsLetterService;
        public NewsLetterController(INewsLetterService newsLetterService)
        {
            this.newsLetterService = newsLetterService;
        }
        public IActionResult Add(Newsletter newsLetter)
        {
            if (!ModelState.IsValid)
                return StatusCode(200, ModelState.Values);
            var exist = newsLetterService.Get(newsLetter.Email);
            if(exist.Status != Service.Utils.ResultStatus.NOT_FOUND)
                return StatusCode(200, "Te-ai înscris deja la photoletter.");
            var addRes = newsLetterService.Add(newsLetter);
            if (!addRes.IsOk())
                return StatusCode((int)addRes.Status);
            return StatusCode(200, "Te-ai înscris cu succes la newsletter!");
        }
    }
}
