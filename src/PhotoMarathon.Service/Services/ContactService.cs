using PhotoMarathon.Data.Entities;
using PhotoMarathon.Data.Infrastructure;
using PhotoMarathon.Data.Repository;
using PhotoMarathon.Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoMarathon.Service.Services
{
    public interface IContactService
    {
        Result Add(ContactMessage contactMessage);
        Result<List<ContactMessage>> GetAll();
    }
    public class ContactService : BaseService, IContactService
    {
        private readonly IEntityBaseRepository<ContactMessage> _contactMessageRepository;
        public ContactService(IUnitOfWork unitOfWork,
            IEntityBaseRepository<ContactMessage> contactMessageRepository):base(unitOfWork)
        {
            this._contactMessageRepository = contactMessageRepository;
        }

        public Result Add(ContactMessage contactMessage)
        {
            try
            {
                contactMessage.DateAdded = DateTime.Now;
                _contactMessageRepository.Add(contactMessage);
                SaveChanges();
                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Result<List<ContactMessage>> GetAll()
        {
            try
            {
                return new Result<List<ContactMessage>>(_contactMessageRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return new Result<List<ContactMessage>>(ex);
            }
        }
    }
}
