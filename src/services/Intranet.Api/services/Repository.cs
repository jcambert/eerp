using EntityFrameworkCore.Triggers;
using Intranet.Api.dbcontext;
using Intranet.Api.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Intranet.Api.services
{
    public interface IRepository<TContext, TEntity>
        where TContext: DbContextWithTriggers
        where TEntity : IdTrackable
    {
        TContext Context { get; }
        DbSet<TEntity> DataSet { get; }

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        TEntity GetByID(object id);
        Task<TEntity> GetByIdAsync(int id);
        void Insert(TEntity entity);
        void Delete(object id);
        Task<TEntity> DeleteAsync(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        bool Exists(int id);
    }

    public class Repository<TContext, TEntity>:IRepository<TContext,TEntity>
        where TContext : DbContextWithTriggers
        where TEntity : IdTrackable
    {
        
        public DbSet<TEntity> DataSet { get; }

        public TContext Context { get; }

        public Repository(TContext context)
        {
            Context = context;
            DataSet = context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await DataSet.FindAsync(id);
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DataSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return DataSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            DataSet.Add(entity);
        }

        public async virtual Task<TEntity> DeleteAsync(object id)
        {
            TEntity entityToDelete = await DataSet.FindAsync(id);
            if (entityToDelete == null) return null;
            Delete(entityToDelete);
            return entityToDelete;
        }


        
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DataSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DataSet.Attach(entityToDelete);
            }
            DataSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DataSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DataSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
        public bool Exists(int id)
        {
            return DataSet.Any(e => e.Id == id);
        }
    }
}
