using Microsoft.EntityFrameworkCore;
using PhotoMarathon.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit.Core;
using PhotoMarathon.Data.Entities.Base;

namespace PhotoMarathon.Data.Repository
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        #region Properties
        private BaseDbContext dataContext;
        private readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected BaseDbContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }
        #endregion

        public EntityBaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        #region IEntityBaseRepository Implementation
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> objects = dbSet.Where<T>(predicate).AsEnumerable();
            foreach (T obj in objects)
            {
                dbSet.Remove(obj);
            }
        }

        public virtual T GetById(int id)
        {
            return dbSet.FirstOrDefault(p => p.Id == id);
        }

        public virtual T GetByIdIncluding(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefault(o => o.Id == id);
        }

        public virtual T Get(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate).FirstOrDefault<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }

        public virtual IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        public virtual IEnumerable<T> FindByIncluding(Expression<Func<T, bool>> predicate, Func<T, object> orderBy, bool ascending, int pageIndex, int pageSize, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            query = query.AsExpandable().Where(predicate);
            query = ascending ? query.OrderBy(orderBy).AsQueryable() : query.OrderByDescending(orderBy).AsQueryable();
            return PaginateSource(query, pageIndex, pageSize).ToList();
        }

        public virtual int Count()
        {
            return dbSet.Count();
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return dbSet.AsExpandable().Where(predicate).Count();
        }
        #endregion

        protected static IQueryable<T> PaginateSource(IQueryable<T> source, int pageIndex, int pageSize)
        {
            return (source.Skip((pageIndex - 1) * pageSize).Take(pageSize));
        }

        public IEnumerable<T> GetAllIncludingPred(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsExpandable().Where(predicate).ToList();
        }
    }
}
