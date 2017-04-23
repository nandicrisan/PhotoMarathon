using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using PhotoMarathon.Data.Entities;
using PhotoMarathon.Models;
using PhotoMarathon.Service.Filters;
using PhotoMarathon.Service.Services;
using PhotoMarathon.Service.Utils;
using PhotoMarathon.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PhotoMarathon.Data.Entities.Cms;
using PhotoMarathon.Data.Entities.Enumes;
using PhotoMarathon.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PhotoMarathon.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IGeneralService _generalService;
        private readonly IAccountService _accountService;
        private readonly INewsLetterService _newsletterService;
        private readonly IBlogService _blogService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IContactService _contactService;
        private readonly ICmsService _cmsService;

        public object AppDomain { get; private set; }

        public AdminController(
            IAccountService accountService,
            INewsLetterService newsletterService,
            IBlogService blogService,
            IHostingEnvironment hostingEnvironment,
            IGeneralService generalService,
            IContactService contactService,
            ICmsService cmsService)
        {
            _accountService = accountService;
            _newsletterService = newsletterService;
            _blogService = blogService;
            _generalService = generalService;
            _hostingEnvironment = hostingEnvironment;
            _contactService = contactService;
            _cmsService = cmsService;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel();
            homeViewModel.PhotograperCount = _accountService.Count(new PhotographerFilter()).Data;
            homeViewModel.NewsletterCount = _newsletterService.Count(new PhotoLetterFilter()).Data;
            return View(homeViewModel);
        }
        public IActionResult Photographers()
        {
            var regStatus = _generalService.GetRegisterStatus();
            if (regStatus.IsOk())
                return View(regStatus.Data);

            return View(new RegisterStatus());
        }
        public IActionResult GetPhotograpers(PhotographerFilter filter)
        {
            //Get the data in a string list filtered by the PhotographerFilter
            var result = _accountService.BuildForDatatable(filter);
            if (!result.IsOk()) return Json("-1");

            // Get the count of all records filtered by the PhotographerFilter
            var count = _accountService.Count(filter);
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
            var result = _newsletterService.BuildForDatatable(filter);
            if (!result.IsOk()) return Json("-1");

            var count = _newsletterService.Count(filter);
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
        #region Blog
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult GetBlogItems(BlogFilter filter)
        {
            var result = _blogService.BuildForDatatable(filter);
            if (!result.IsOk()) return Json("-1");

            var count = _blogService.Count(filter);
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
        public IActionResult AddBlogItem(int id = 0)
        {
            var blogItem = new BlogItem();
            if (id != 0)
            {
                blogItem = _blogService.Get(id).Data;
            }
            return View(blogItem);
        }
        public IActionResult DeleteBlog(int id)
        {
            var deleteRes = _blogService.Delete(id);
            if (deleteRes.IsOk())
                return StatusCode((int)deleteRes.Status, deleteRes.Message);
            return StatusCode(200);
        }
        [HttpPost]
        public IActionResult AddBlogItem(BlogItem blogitem)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            if (!ModelState.IsValid)
                return View(blogitem);
            var file = Request.Form.Files["main-image"];
            if (file.Length != 0)
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                string fileName, savedName;
                fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(fileName);
                savedName = Guid.NewGuid().ToString() + extension;
                var path = Path.Combine(webRootPath + "\\images\\blog\\", savedName);
                blogitem.MainImageName = savedName;
                using (FileStream fs = System.IO.File.Create(path))
                {
                    file.CopyTo(fs);
                    fs.Flush();
                }
            }
            Result<BlogItem> addRes;
            //Set slug
            blogitem.Slug = WebUtils.GenerateSlug(blogitem.Title);
            if (blogitem.Id == 0)
                addRes = _blogService.Add(blogitem);
            else
                addRes = _blogService.Edit(blogitem);
            if (addRes.IsOk())
            {
                TempData.Add("Message", "Salvat");
                TempData.Add("ServerMessageType", "success");
            }
            else
            {
                TempData.Add("Message", addRes.Message ?? "");
                TempData.Add("ServerMessageType", "error");
            }
            return RedirectToAction("Blog");
        }
        #endregion
        public IActionResult Suggestions()
        {
            var messList = _contactService.GetAll();
            if (!messList.IsOk())
                return StatusCode(500, messList.Message);
            return View(messList.Data);
        }

        public IActionResult Cms()
        {
            //Generate tree model
            var pages = _cmsService.GetAllPages();
            if(!pages.IsOk())
                return new StatusCodeResult(500);
            var tree =new List<Node>();
            foreach (var page in pages.Data)
            {
                var pageNode = new Node
                {
                    text = page.Name,
                    id = page.Id,
                    type = CmsStructureType.Page
                };
                if (page.Sections != null)
                {
                    pageNode.nodes = new List<Node>();
                    foreach (var section in page.Sections)
                    {
                        var sectionNode = new Node
                        {
                            text = section.Name,
                            id = section.Id,
                            type = CmsStructureType.Section
                        };
                        if (section.Articles != null)
                        {
                            sectionNode.nodes = new List<Node>();
                            foreach (var article in section.Articles)
                            {
                                sectionNode.nodes.Add(new Node
                                {
                                    text = article.Name,
                                    id = article.Id,
                                    type = CmsStructureType.Article
                                });
                            }
                        }
                        pageNode.nodes.Add(sectionNode);
                    }
                }
                tree.Add(pageNode);
            }
            ViewBag.Tree = JsonConvert.SerializeObject(tree);
            return View(tree);
        }

        public string GetCmsData(CmsStructureType type, int id)
        {
            switch (type)
            {
                case CmsStructureType.Undefined:
                    return "";
                case CmsStructureType.Page:
                    var page = _cmsService.GetPage(id);
                    return JsonConvert.SerializeObject(page.Data);
                case CmsStructureType.Section:
                    var section = _cmsService.GetSection(id);
                    return JsonConvert.SerializeObject(section.Data);
                case CmsStructureType.Article:
                    var article = _cmsService.GetArticle(id);
                    return JsonConvert.SerializeObject(article.Data);
                default:
                    break;
                    return "";
            }
            return "";
        }
        public string SaveCmsData(CmsStructureType type, int id, string content, string title, string subtitle, string slug, string name)
        {
            switch (type)
            {
                case CmsStructureType.Undefined:
                    return "";
                case CmsStructureType.Page:
                    var page = new Page
                    {
                        Title = title,
                        Subtitle = subtitle,
                        Id = id,
                        Slug = slug,
                        Name = name
                    };
                    _cmsService.EditPage(page);
                    return JsonConvert.SerializeObject("");
                case CmsStructureType.Section:
                    var section = new Section
                    {
                        Content = content,
                        Title = title,
                        Subtitle = subtitle,
                        Id = id,
                        Slug=slug,
                        Name = name
                    };
                    _cmsService.EditSection(section);
                    return JsonConvert.SerializeObject("");
                case CmsStructureType.Article:
                    var article = new Article
                    {
                        Content = content,
                        Title = title,
                        Subtitle = subtitle,
                        Id = id,
                        Slug = slug,
                        Name = name
                    };
                    _cmsService.EditArticle(article);
                    return JsonConvert.SerializeObject("");
                default:
                    break;
            }
            return "";
        }
    }
}