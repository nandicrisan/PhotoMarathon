using LinqKit.Core;
using PhotoMarathon.Data.Entities;
using PhotoMarathon.Data.Infrastructure;
using PhotoMarathon.Data.Repository;
using PhotoMarathon.Service.Filters;
using PhotoMarathon.Service.Utils;
using System;
using System.Collections.Generic;
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
                var content = blogItem.Content;
                if (content.Length > 350)
                    content = content.Substring(0, 349) + "...";

                aaData.Add(new string[] {
                    blogItem.Title,
                    blogItem.CreatedBy,
                    blogItem.DateAdded.ToString(),
                    content
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

        #region private functions
        private static Expression<Func<BlogItem, bool>> CreatePredicate(BlogFilter filter)
        {
            var predicate = PredicateBuilder.True<BlogItem>();
            if (filter == null) return predicate;
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
                case 0:
                    return new Func<BlogItem, object>(c => c.Title);
                case 1:
                    return new Func<BlogItem, object>(c => c.CreatedBy);
                default:
                    return new Func<BlogItem, object>(q => q.DateAdded);
            }
        }
        #endregion
    }
}
