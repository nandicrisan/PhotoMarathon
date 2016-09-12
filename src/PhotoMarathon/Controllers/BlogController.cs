using Microsoft.AspNetCore.Mvc;
using PhotoMarathon.Service.Filters;
using PhotoMarathon.Service.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoMarathon.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }
        public IActionResult Index(int p = 1)
        {
            var filter = new BlogFilter();
            filter.iPage = p;
            filter.iDisplayStart = p;
            filter.iDisplayLength = 6;
            ViewBag.Pages = (blogService.Count(filter).Data / 6) + 1;
            ViewBag.CurrentPage = p;
            var blogItems = blogService.GetBlogItemsByFilter(filter);
            return View(blogItems.Data);
        }
        public IActionResult Article(int id)
        {
            var blogItem = blogService.Get(id);
            return View(blogItem.Data);
        }
    }
}
