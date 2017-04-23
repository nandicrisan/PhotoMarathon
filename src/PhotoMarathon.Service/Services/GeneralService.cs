using PhotoMarathon.Data.Entities;
using PhotoMarathon.Data.Infrastructure;
using PhotoMarathon.Data.Repository;
using PhotoMarathon.Service.Utils;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PhotoMarathon.Service.Services
{
    public interface IGeneralService : IBaseService
    {
        Result<List<WorkShop>> GetWorkShpos();
        Result<RegisterStatus> GetRegisterStatus();
        Result<RegisterStatus> SetRegisterStatus(RegisterStatus model);
    }

    public class GeneralService : BaseService, IGeneralService
    {
        private readonly IEntityBaseRepository<WorkShop> _workShopRepository;
        private readonly IEntityBaseRepository<RegisterStatus> _registerStatusRepository;
        public GeneralService(IEntityBaseRepository<WorkShop> workShopRepository, IEntityBaseRepository<RegisterStatus> registerStatusRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _workShopRepository = workShopRepository;
            _registerStatusRepository = registerStatusRepository;
        }

        public Result<RegisterStatus> GetRegisterStatus()
        {
            var registerStatus = _registerStatusRepository.GetAll().ToList();
            if (registerStatus.Any())
                return new Result<RegisterStatus>(registerStatus.FirstOrDefault());
            return new Result<RegisterStatus>(ResultStatus.EMPTY);
        }

        public Result<List<WorkShop>> GetWorkShpos()
        {
            var workShops = _workShopRepository.GetAll();
            return new Result<List<WorkShop>>(workShops.ToList());
        }

        public Result<RegisterStatus> SetRegisterStatus(RegisterStatus model)
        {
            try
            {
                _registerStatusRepository.Update(model);
                SaveChanges();
                return new Result<RegisterStatus>(ResultStatus.OK);
            }
            catch (Exception ex)
            {
                return new Result<RegisterStatus>(ResultStatus.ERROR, ex.Message);
            }
        }
    }
}
