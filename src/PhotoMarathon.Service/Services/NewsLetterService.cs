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
    public interface INewsLetterService : IBaseService
    {
        Result<Newsletter> Add(Newsletter newsLetter);
        Result<Newsletter> Get(string email);
        Result<List<Newsletter>> GetNewslettersByFilter(PhotoLetterFilter filter);
        Result<List<string[]>> BuildForDatatable(PhotoLetterFilter filter);
        Result<int> Count(PhotoLetterFilter filter);
    }
    public class NewsLetterService : BaseService, INewsLetterService
    {
        private readonly IEntityBaseRepository<Newsletter> newsLetterRepository;

        public NewsLetterService(
            IEntityBaseRepository<Newsletter> newsLetterRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.newsLetterRepository = newsLetterRepository;
        }

        public Result<Newsletter> Add(Newsletter newsLetter)
        {
            try
            {
                newsLetter.DateAdded = DateTime.Now;
                newsLetterRepository.Add(newsLetter);
                SaveChanges();
                return new Result<Newsletter>(newsLetter);
            }
            catch (Exception ex)
            {
                return new Result<Newsletter>(ex);
            }
        }

        public Result<Newsletter> Get(string email)
        {
            try
            {
                Expression<Func<Newsletter, bool>> predicate = p => p.Email.ToLower() == email.ToLower();
                var newsletter = newsLetterRepository.Get(predicate);
                if (newsletter == null)
                    return new Result<Newsletter>(ResultStatus.NOT_FOUND);
                return new Result<Newsletter>(newsletter);
            }
            catch (Exception ex)
            {
                return new Result<Newsletter>(ex);
            }
        }

        public Result<List<Newsletter>> GetNewslettersByFilter(PhotoLetterFilter filter)
        {
            try
            {
                var predicate = CreatePredicate(filter);
                var orderby = GetSortedFunction(filter);
                var orderDirection = GetOrderDirection(filter);
                var newsletters = newsLetterRepository.FindByIncluding(predicate, orderby, orderDirection, filter.iDisplayStart, filter.iDisplayLength) as List<Newsletter>;
                return new Result<List<Newsletter>>(newsletters);
            }
            catch (Exception ex)
            {
                return new Result<List<Newsletter>>(ex);
            }
        }

        public Result<List<string[]>> BuildForDatatable(PhotoLetterFilter filter)
        {
            var aaData = new List<string[]>();

            // Setting data for pagination
            if (Convert.ToInt32(filter.iDisplayStart) == 0)
                filter.iDisplayStart = 1;
            else
                filter.iDisplayStart = Convert.ToInt32(filter.iDisplayStart) / Convert.ToInt32(filter.iDisplayLength) + 1;

            // Getting the Cases
            var findResult = GetNewslettersByFilter(filter);
            if (!findResult.IsOk()) return new Result<List<string[]>>(findResult.Status);
            foreach (var photographer in findResult.Data)
            {
                aaData.Add(new string[] {
                    photographer.Name,
                    photographer.Email,
                    photographer.DateAdded.ToString(),
                });
            }
            return new Result<List<string[]>>(aaData);
        }

        public Result<int> Count(PhotoLetterFilter filter)
        {
            try
            {
                var predicate = CreatePredicate(filter);
                var count = newsLetterRepository.Count(predicate);
                return new Result<int>(count);
            }
            catch (Exception ex)
            {
                return new Result<int>(ex);
            }
        }

        #region private functions
        private static Expression<Func<Newsletter, bool>> CreatePredicate(PhotoLetterFilter filter)
        {
            var predicate = PredicateBuilder.True<Newsletter>();
            if (filter == null) return predicate;
            return predicate;
        }
        private static bool GetOrderDirection(PhotoLetterFilter filter)
        {
            return filter == null || filter.sSortDir_0 != "desc";
        }
        private static Func<Newsletter, object> GetSortedFunction(PhotoLetterFilter filter)
        {
            if (filter == null) return new Func<Newsletter, object>(q => q.DateAdded);
            switch (filter.iSortCol_0)
            {
                case 0:
                    return new Func<Newsletter, object>(c => c.Name);
                case 1:
                    return new Func<Newsletter, object>(c => c.Email);
                default:
                    return new Func<Newsletter, object>(q => q.DateAdded);
            }
        }
        #endregion
    }
}
