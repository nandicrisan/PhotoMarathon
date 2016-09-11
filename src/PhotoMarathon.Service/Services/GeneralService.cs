using PhotoMarathon.Data.Entities;
using PhotoMarathon.Data.Infrastructure;
using PhotoMarathon.Data.Repository;
using PhotoMarathon.Service.Utils;
using System.Collections.Generic;
using System.Linq;

namespace PhotoMarathon.Service.Services
{
    public interface IGeneralService : IBaseService
    {
        Result<List<WorkShop>> GetWorkShpos ();
    }

    public class GeneralService : BaseService, IGeneralService
    {
        private readonly IEntityBaseRepository<WorkShop> workShopRepository;
        public GeneralService(
            IEntityBaseRepository<WorkShop> workShopRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.workShopRepository = workShopRepository;
        }
        public Result<List<WorkShop>> GetWorkShpos()
        {
            var workShops = workShopRepository.GetAll();
            return new Result<List<WorkShop>>(workShops.ToList());
        }
    }
}
