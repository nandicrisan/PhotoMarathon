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
            var addRes = newsLetterService.Add(newsLetter);
            if (!addRes.IsOk())
                return StatusCode((int)addRes.Status);
            return StatusCode(200);
        }
    }
}
