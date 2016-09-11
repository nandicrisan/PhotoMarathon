using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using PhotoMarathon.Data.Entities;
using PhotoMarathon.Service.Filters;
using PhotoMarathon.Service.Services;
using System;
using System.IO;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoMarathon.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IAccountService accountService;
        private readonly INewsLetterService newsletterService;
        private readonly IBlogService blogService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public object AppDomain { get; private set; }

        public AdminController(
            IAccountService accountService,
            INewsLetterService newsletterService,
            IBlogService blogService,
            IHostingEnvironment hostingEnvironment)
        {
            this.accountService = accountService;
            this.newsletterService = newsletterService;
            this.blogService = blogService;
            this._hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Photographers()
        {
            return View();
        }
        public IActionResult GetPhotograpers(PhotographerFilter filter)
        {
            //Get the data in a string list filtered by the PhotographerFilter
            var result = accountService.BuildForDatatable(filter);
            if (!result.IsOk()) return Json("-1");

            // Get the count of all records filtered by the PhotographerFilter
            var count = accountService.Count(filter);
            if (!count.IsOk()) return Json("-1");
            var iTotalRecords = count.Data;
            var iTotalDisplayRecords = count.Data;

            // Return the data for tha datatable in a json object
            return Json(new
            {
                filter.sEcho,
                iTotalRecords,
                iTotalDisplayRecords,
                aaData = result.Data
            });
        }
        public IActionResult Photoletter()
        {
            return View();
        }
        public IActionResult GetPhotoletters(PhotoLetterFilter filter)
        {
            var result = newsletterService.BuildForDatatable(filter);
            if (!result.IsOk()) return Json("-1");

            var count = newsletterService.Count(filter);
            if (!count.IsOk()) return Json("-1");
            var iTotalRecords = count.Data;
            var iTotalDisplayRecords = count.Data;

            return Json(new
            {
                filter.sEcho,
                iTotalRecords,
                iTotalDisplayRecords,
                aaData = result.Data
            });
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult GetBlogItems(BlogFilter filter)
        {
            var result = blogService.BuildForDatatable(filter);
            if (!result.IsOk()) return Json("-1");

            var count = blogService.Count(filter);
            if (!count.IsOk()) return Json("-1");
            var iTotalRecords = count.Data;
            var iTotalDisplayRecords = count.Data;

            return Json(new
            {
                filter.sEcho,
                iTotalRecords,
                iTotalDisplayRecords,
                aaData = result.Data
            });
        }
        public IActionResult AddBlogItem()
        {
            var blogItem = new BlogItem();
            return View(blogItem);
        }
        [HttpPost]
        public IActionResult AddBlogItem(BlogItem blogitem)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            if (!ModelState.IsValid)
                return View(blogitem);
            var file = Request.Form.Files["main-image"];
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            string fileName, savedName;
            fileName = Path.GetFileName(file.FileName);
            var extension = Path.GetExtension(fileName);
            savedName = Guid.NewGuid().ToString() + extension;
            var path = Path.Combine(webRootPath+"\\images\\blog\\", savedName);
            blogitem.MainImageName = savedName;
            using (FileStream fs = System.IO.File.Create(path))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            //Success verification
            blogService.Add(blogitem);
            return RedirectToAction("Blog");
        }
    }
}
