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
    public interface IAccountService : IBaseService
    {
        Result<Photographer> AddPhotographer(Photographer photographer);
        Result<List<Photographer>> GetPhotographersByFilter(PhotographerFilter filter);
        Result<List<string[]>> BuildForDatatable(PhotographerFilter filter);
        Result<int> Count(PhotographerFilter filter);
    }

    public class AccountService : BaseService, IAccountService
    {
        private readonly IEntityBaseRepository<Photographer> photographerRepository;
        public AccountService(IEntityBaseRepository<Photographer> photographerRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.photographerRepository = photographerRepository;
        }
        public Result<Photographer> AddPhotographer(Photographer photographer)
        {
            try
            {
                photographerRepository.Add(photographer);
                photographer.DateAdded = DateTime.Now;
                this.SaveChanges();
                return new Result<Photographer>(photographer);
            }
            catch (Exception ex)
            {
                return new Result<Photographer>(ex);
            }
        }
        public Result<List<Photographer>> GetPhotographersByFilter(PhotographerFilter filter)
        {
            try
            {
                var predicate = CreatePredicate(filter);
                var orderby = GetSortedFunction(filter);
                var orderDirection = GetOrderDirection(filter);
                var photographers = photographerRepository.FindByIncluding(predicate, orderby, orderDirection, filter.iDisplayStart, filter.iDisplayLength, pageIndex => pageIndex.Workshop) as List<Photographer>;
                return new Result<List<Photographer>>(photographers);
            }
            catch (Exception ex)
            {
                return new Result<List<Photographer>>(ex);
            }
        }
        public Result<List<string[]>> BuildForDatatable(PhotographerFilter filter)
        {
            var aaData = new List<string[]>();

            // Setting data for pagination
            if (Convert.ToInt32(filter.iDisplayStart) == 0)
                filter.iDisplayStart = 1;
            else
                filter.iDisplayStart = Convert.ToInt32(filter.iDisplayStart) / Convert.ToInt32(filter.iDisplayLength) + 1;

            // Getting the Cases
            var findResult = GetPhotographersByFilter(filter);
            if (!findResult.IsOk()) return new Result<List<string[]>>(findResult.Status);
            foreach (var photographer in findResult.Data)
            {
                var workshopName = "-";
                if (photographer.RegisterForWorkShop && photographer.Workshop != null)
                    workshopName = photographer.Workshop.Name;
                aaData.Add(new string[] {
                    photographer.FirstName,
                    photographer.LastName,
                    photographer.Email,
                    photographer.PhoneNumber,
                    photographer.DateAdded.ToString(),
                    photographer.IsProfessionist.ToString(),
                    workshopName,
                    photographer.RegisterForMarathon.ToString(),
                    photographer.Age.ToString(),
                    photographer.City,
                    photographer.Camera,
                    photographer.EditionId.ToString()
                });
            }
            return new Result<List<string[]>>(aaData);
        }
        public Result<int> Count(PhotographerFilter filter)
        {
            try
            {
                var predicate = CreatePredicate(filter);
                var count = photographerRepository.Count(predicate);
                return new Result<int>(count);
            }
            catch (Exception ex)
            {
                return new Result<int>(ex);
            }
        }

        #region private functions
        private static Expression<Func<Photographer, bool>> CreatePredicate(PhotographerFilter filter)
        {
            var predicate = PredicateBuilder.True<Photographer>();
            if (filter == null) return predicate;
            return predicate;
        }
        private static bool GetOrderDirection(PhotographerFilter filter)
        {
            return filter == null || filter.sSortDir_0 != "desc";
        }
        private static Func<Photographer, object> GetSortedFunction(PhotographerFilter filter)
        {
            if (filter == null) return new Func<Photographer, object>(q => q.DateAdded);
            switch (filter.iSortCol_0)
            {
                case 0:
                    return new Func<Photographer, object>(c => c.FirstName);
                case 1:
                    return new Func<Photographer, object>(c => c.LastName);
                case 2:
                    return new Func<Photographer, object>(c => c.Email);
                case 4:
                    return new Func<Photographer, object>(c => c.DateAdded);
                default:
                    return new Func<Photographer, object>(q => q.DateAdded);
            }
        }
        #endregion
    }
}
