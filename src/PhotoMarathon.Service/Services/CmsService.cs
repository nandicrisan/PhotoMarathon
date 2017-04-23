using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PhotoMarathon.Data.Entities.Cms;
using PhotoMarathon.Data.Infrastructure;
using PhotoMarathon.Data.Repository;
using PhotoMarathon.Service.Utils;

namespace PhotoMarathon.Service.Services
{
    public interface ICmsService
    {
        Result<Page> GetPage(string slug);
        Result<List<Page>> GetAllPages();
        Result<Page> GetPage(int id);
        Result<Section> GetSection(int id);
        Result<Article> GetArticle(int id);
        Result<Page> EditPage(Page page);
        Result<Section> EditSection(Section section);
        Result<Article> EditArticle(Article article);
    }

    public class CmsService : BaseService, ICmsService
    {
        private readonly IEntityBaseRepository<Page> _cmsPageRepository;
        private readonly IEntityBaseRepository<Section> _cmsSectionRepository;
        private readonly IEntityBaseRepository<Article> _cmsArticleRepository;
        private readonly ICmsRepository _cmsRepository;

        public CmsService(IUnitOfWork unitOfWork,
            IEntityBaseRepository<Page> cmsPageRepository,
            IEntityBaseRepository<Section> cmsSectionRepository,
            IEntityBaseRepository<Article> cmsArticleRepository,
            ICmsRepository cmsRepository) : base(unitOfWork)
        {
            _cmsPageRepository = cmsPageRepository;
            _cmsSectionRepository = cmsSectionRepository;
            _cmsArticleRepository = cmsArticleRepository;
            _cmsRepository = cmsRepository;
        }

        public Result<Page> GetPage(string slug)
        {
            try
            {
                var page = _cmsRepository.GetPage(slug);
                if (page == null)
                    return new Result<Page>(ResultStatus.NOT_FOUND);
                return new Result<Page>(page);
            }
            catch (Exception ex)
            {
                return new Result<Page>(ex);
            }
        }

        public Result<List<Page>> GetAllPages()
        {
            try
            {
                var pages = _cmsRepository.GetAllPages();
                return new Result<List<Page>>(pages);
            }
            catch (Exception ex)
            {
                return new Result<List<Page>>(ex);
            }
        }

        public Result<Page> GetPage(int id)
        {
            try
            {
                var page = _cmsPageRepository.GetById(id);
                if (page == null)
                    return new Result<Page>(ResultStatus.NOT_FOUND);
                return new Result<Page>(page);
            }
            catch (Exception ex)
            {
                return new Result<Page>(ex);
            }
        }

        public Result<Section> GetSection(int id)
        {
            try
            {
                var section = _cmsSectionRepository.GetById(id);
                if (section == null)
                    return new Result<Section>(ResultStatus.NOT_FOUND);
                return new Result<Section>(section);
            }
            catch (Exception ex)
            {
                return new Result<Section>(ex);
            }
        }

        public Result<Article> GetArticle(int id)
        {
            try
            {
                var article = _cmsArticleRepository.GetById(id);
                if (article == null)
                    return new Result<Article>(ResultStatus.NOT_FOUND);
                return new Result<Article>(article);
            }
            catch (Exception ex)
            {
                return new Result<Article>(ex);
            }
        }

        public Result<Page> EditPage(Page page)
        {
            try
            {
                page.DateModified = DateTime.Now;
                _cmsPageRepository.Update(page);
                SaveChanges();
                return new Result<Page>(page);
            }
            catch (Exception ex)
            {
                return new Result<Page>(ex);
            }
        }

        public Result<Section> EditSection(Section section)
        {
            try
            {
                section.DateModified = DateTime.Now;
                _cmsSectionRepository.Update(section);
                SaveChanges();
                return new Result<Section>(section);
            }
            catch (Exception ex)
            {
                return new Result<Section>(ex);
            }
        }

        public Result<Article> EditArticle(Article article)
        {
            try
            {
                article.DateModified = DateTime.Now;
                _cmsArticleRepository.Update(article);
                SaveChanges();
                return new Result<Article>(article);
            }
            catch (Exception ex)
            {
                return new Result<Article>(ex);
            }
        }
    }
}
