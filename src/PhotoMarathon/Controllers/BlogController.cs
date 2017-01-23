using Microsoft.AspNetCore.Mvc;
using PhotoMarathon.Models;
using PhotoMarathon.Service.Filters;
using PhotoMarathon.Service.ServiceModel;
using PhotoMarathon.Service.Services;
using System;
using System.Collections.Generic;

namespace PhotoMarathon.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }
        public IActionResult Index(int p = 1, int y = 0, int m = 0)
        {
            var viewModel = new BlogViewModel();
            viewModel.DateFilter = new List<DateFilter>();
            var filter = new BlogFilter();
            filter.iPage = p;
            filter.iDisplayStart = p;
            filter.iDisplayLength = 6;
            filter.sSortDir_0 = "desc";
            if (y == 0)
                y = DateTime.Now.Year;
            viewModel.SelectedYear = y;
            viewModel.SelectedMonth = m;
            filter.start = new DateTime(y, 1, 1);
            filter.end = filter.start.Value.AddYears(1);
            if (m != 0)
            {
                filter.start = filter.start.Value.AddMonths(m - 1);
                filter.end = filter.start.Value.AddMonths(1);
            }
            viewModel.Pages = (blogService.Count(filter).Data / 6) + 1;
            viewModel.CurrentPage = p;
            var blogItems = blogService.GetBlogItemsByFilter(filter);
            if (!blogItems.IsOk())
                return new StatusCodeResult((int)blogItems.Status);
            viewModel.Articles = blogItems.Data;
            //Date filter
            var dateFilter = blogService.GetDateFilter();
            if (dateFilter.IsOk())
                viewModel.DateFilter = dateFilter.Data;
            return View(viewModel);
        }

        public IActionResult Article(string id)
        {
            var blogItem = blogService.Get(id);
            return View(blogItem.Data);
        }
    }
}
