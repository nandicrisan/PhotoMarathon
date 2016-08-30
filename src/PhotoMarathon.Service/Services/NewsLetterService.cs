using PhotoMarathon.Data.Entities;
using PhotoMarathon.Data.Infrastructure;
using PhotoMarathon.Data.Repository;
using PhotoMarathon.Service.Utils;
using System;
using System.Linq.Expressions;

namespace PhotoMarathon.Service.Services
{
    public interface INewsLetterService : IBaseService
    {
        Result<Newsletter> Add(Newsletter newsLetter);
        Result<Newsletter> Get(string email);
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
    }
}
