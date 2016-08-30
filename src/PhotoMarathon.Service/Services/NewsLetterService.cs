using PhotoMarathon.Data.Entities;
using PhotoMarathon.Data.Infrastructure;
using PhotoMarathon.Data.Repository;
using PhotoMarathon.Service.Utils;
using System;

namespace PhotoMarathon.Service.Services
{
    public interface INewsLetterService : IBaseService
    {
        Result<Newsletter> Add(Newsletter newsLetter);
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
                newsLetterRepository.Add(newsLetter);
                SaveChanges();
                return new Result<Newsletter>(newsLetter);
            }
            catch (Exception ex)
            {
                return new Result<Newsletter>(ex); 
            }
        }
    }
}
