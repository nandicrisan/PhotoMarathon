using LinqKit.Core;
using PhotoMarathon.Data.Entities;
using PhotoMarathon.Data.Infrastructure;
using PhotoMarathon.Data.Repository;
using PhotoMarathon.Service.Filters;
using PhotoMarathon.Service.ServiceModel;
using PhotoMarathon.Service.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;

namespace PhotoMarathon.Service.Services
{
    public interface IBlogService
    {
        Result<BlogItem> Add(BlogItem blogItem);
        Result<List<BlogItem>> GetBlogItemsByFilter(BlogFilter filter);
        Result<List<string[]>> BuildForDatatable(BlogFilter filter);
        Result<int> Count(BlogFilter filter);
        Result<BlogItem> Get(int id);
        Result<BlogItem> Get(string id);
        Result Delete(int id);
        Result<BlogItem> Edit(BlogItem blogItem);
        Result<List<DateFilter>> GetDateFilter();
    }
    public class BlogService : BaseService, IBlogService
    {
        private readonly IEntityBaseRepository<BlogItem> blogRepository;

        public BlogService(IUnitOfWork unitOfWork,
            IEntityBaseRepository<BlogItem> blogRepository) : base(unitOfWork)
        {
            this.blogRepository = blogRepository;
        }
        public Result<BlogItem> Add(BlogItem blogItem)
        {
            try
            {
                blogItem.DateAdded = DateTime.Now;
                blogRepository.Add(blogItem);
                SaveChanges();
                return new Result<BlogItem>(blogItem);
            }
            catch (Exception ex)
            {
                return new Result<BlogItem>(ex);
            }
        }

        public Result<List<BlogItem>> GetBlogItemsByFilter(BlogFilter filter)
        {
            try
            {
                var predicate = CreatePredicate(filter);
                var orderby = GetSortedFunction(filter);
                var orderDirection = GetOrderDirection(filter);
                var blogItems = blogRepository.FindByIncluding(predicate, orderby, orderDirection, filter.iDisplayStart, filter.iDisplayLength) as List<BlogItem>;
                return new Result<List<BlogItem>>(blogItems);
            }
            catch (Exception ex)
            {
                return new Result<List<BlogItem>>(ex);
            }
        }

        public Result<List<string[]>> BuildForDatatable(BlogFilter filter)
        {
            var aaData = new List<string[]>();

            // Setting data for pagination
            if (Convert.ToInt32(filter.iDisplayStart) == 0)
                filter.iDisplayStart = 1;
            else
                filter.iDisplayStart = Convert.ToInt32(filter.iDisplayStart) / Convert.ToInt32(filter.iDisplayLength) + 1;

            // Getting the Cases
            var findResult = GetBlogItemsByFilter(filter);
            if (!findResult.IsOk()) return new Result<List<string[]>>(findResult.Status);
            foreach (var blogItem in findResult.Data)
            {
                aaData.Add(new string[] {
                    blogItem.Title,
                    blogItem.CreatedBy,
                    blogItem.DateAdded.ToString(),
                    blogItem.ShortDescription,
                    blogItem.Id.ToString()
                });
            }
            return new Result<List<string[]>>(aaData);
        }

        public Result<int> Count(BlogFilter filter)
        {
            try
            {
                var predicate = CreatePredicate(filter);
                var count = blogRepository.Count(predicate);
                return new Result<int>(count);
            }
            catch (Exception ex)
            {
                return new Result<int>(ex);
            }
        }

        public Result<BlogItem> Get(int id)
        {
            try
            {
                return new Result<BlogItem>(blogRepository.GetById(id));
            }
            catch (Exception ex)
            {
                return new Result<BlogItem>(ex);
            }
        }

        public Result<BlogItem> Get(string slug)
        {
            try
            {
                Expression<Func<BlogItem, bool>> predicate = p => p.Slug == slug;
                    return new Result<BlogItem>(blogRepository.Get(predicate));
            }
            catch (Exception ex)
            {
                return new Result<BlogItem>(ex);
            }
        }

        public Result Delete(int id)
        {
            try
            {
                var blogItem = blogRepository.GetById(id);
                if (blogItem == null)
                    return new Result(ResultStatus.NOT_FOUND);
                blogRepository.Delete(blogItem);
                SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Result<BlogItem> Edit(BlogItem blogItem)
        {
            try
            {
                blogRepository.Update(blogItem);
                SaveChanges();
                return new Result<BlogItem>(blogItem);
            }
            catch (Exception ex)
            {
                return new Result<BlogItem>(ex);
            }
        }

        public Result<List<DateFilter>> GetDateFilter()
        {
            try
            {
                var start = new DateTime(2016, 8, 1);
                var tempDate = DateTime.Now.AddMonths(1);
                var end = new DateTime(tempDate.Year, tempDate.Month, 1);
                var currentYear = 2016;
                var returnDates = new List<DateFilter>();
                DateFilter tempDateFilter = null;
                for (DateTime date = start; date.Date <= end.Date; date = date.AddMonths(1))
                {
                    Expression<Func<BlogItem, bool>> predicate = p => p.DateAdded > date && p.DateAdded < date.AddMonths(1);
                    if (date.Year == currentYear)
                    {
                        if (tempDateFilter != null)
                            returnDates.Add(tempDateFilter);
                        tempDateFilter = new DateFilter
                        {
                            Year = currentYear,
                            Months = new List<MonthFilter>()
                        };
                        currentYear++;
                    }
                    var articleCount = blogRepository.Count(predicate);
                    tempDateFilter.Months.Add(new MonthFilter
                    {
                        Month = date.Month,
                        Items = articleCount,
                        MonthName = date.ToString("MMM", CultureInfo.DefaultThreadCurrentUICulture)
                    });
                }
                returnDates.Add(tempDateFilter);
                return new Result<List<DateFilter>>(returnDates);
            }
            catch (Exception ex)
            {
                return new Result<List<DateFilter>>(ex);
            }
        }

        #region private functions
        private static Expression<Func<BlogItem, bool>> CreatePredicate(BlogFilter filter)
        {
            var predicate = PredicateBuilder.True<BlogItem>();
            if (filter == null) return predicate;
            if (filter.start != null && filter.end != null)
                predicate = predicate.And(p => p.DateAdded > filter.start.Value && p.DateAdded < filter.end.Value);
            return predicate;
        }
        private static bool GetOrderDirection(BlogFilter filter)
        {
            return filter == null || filter.sSortDir_0 != "desc";
        }
        private static Func<BlogItem, object> GetSortedFunction(BlogFilter filter)
        {
            if (filter == null) return new Func<BlogItem, object>(q => q.DateAdded);
            switch (filter.iSortCol_0)
            {
                case 1:
                    return new Func<BlogItem, object>(c => c.Title);
                case 2:
                    return new Func<BlogItem, object>(c => c.CreatedBy);
                default:
                    return new Func<BlogItem, object>(q => q.DateAdded);
            }
        }
        #endregion
    }
}
