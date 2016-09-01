using PhotoMarathon.Data.Entities;
using PhotoMarathon.Data.Infrastructure;
using PhotoMarathon.Data.Repository;
using PhotoMarathon.Service.Utils;
using System;

namespace PhotoMarathon.Service.Services
{
    public interface IAccountService : IBaseService
    {
        Result<Photographer> AddPhotographer(Photographer photographer);
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
    }
}
