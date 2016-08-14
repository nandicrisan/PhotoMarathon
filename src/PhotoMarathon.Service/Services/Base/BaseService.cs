using PhotoMarathon.Data.Infrastructure;

namespace PhotoMarathon.Service.Services.Base
{


    public abstract class BaseService : IBaseService
    {
        #region Properties
        protected readonly IUnitOfWork unitOfWork;
        #endregion

        public BaseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region IBaseService Members
        public virtual void SaveChanges()
        {
            unitOfWork.Commit();
        }
        #endregion
    }
}
