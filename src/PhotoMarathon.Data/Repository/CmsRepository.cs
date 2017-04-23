using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoMarathon.Data.Entities.Cms;

namespace PhotoMarathon.Data.Repository
{
    public interface ICmsRepository
    {
        Page GetPage(string slug);
        List<Page> GetAllPages();
    }

    public class CmsRepository : ICmsRepository
    {
        #region Properties

        private readonly BaseDbContext _dataContext;

        #endregion

        public CmsRepository(BaseDbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public Page GetPage(string slug)
        {
            var page = _dataContext.Set<Page>()
                .Include(p => p.Sections)
                .ThenInclude(p => p.Articles)
                .FirstOrDefault(p => p.Slug == slug);
            return page;
        }
        public List<Page> GetAllPages()
        {
            var pages = _dataContext.Set<Page>()
                .Include(p => p.Sections)
                .ThenInclude(p => p.Articles).ToList();
            return pages;
        }
    }
}
