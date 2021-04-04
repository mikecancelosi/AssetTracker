using AssetTracker.Model;
using Mehdime.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssetTracker.DAL
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        private IDbContextScopeFactory dbContextScopeFactory;

        public GenericRepository(IDbContextScopeFactory contextScopeFactory)
        {
            dbContextScopeFactory = contextScopeFactory;
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
            )
        {
            using (var dbContextScope = dbContextScopeFactory.CreateReadOnly())
            {
                DbContext context = dbContextScope.DbContexts.Get<TrackerContext>();
                IQueryable<TEntity> query = context.Set<TEntity>();

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
        }

        public virtual TEntity GetByID(object id)
        {
            using (var dbContextScope = dbContextScopeFactory.CreateReadOnly())
            {
                DbContext context = dbContextScope.DbContexts.Get<TrackerContext>();
                return context.Set<TEntity>().Find(id);
            }
        }

        public virtual void Insert(TEntity entity)
        {
            using (var dbContextScope = dbContextScopeFactory.Create())
            {
                DbContext context = dbContextScope.DbContexts.Get<TrackerContext>();
                context.Set<TEntity>().Add(entity);
                dbContextScope.SaveChanges();
            }
        }

        public virtual void Insert(TEntity entity,Type type)
        {
            using (var dbContextScope = dbContextScopeFactory.Create())
            {
                DbContext context = dbContextScope.DbContexts.Get<TrackerContext>();
                context.Set(type).Add(entity);
                dbContextScope.SaveChanges();
            }
        }

        public virtual void Delete( object id)
        {
            using (var dbContextScope = dbContextScopeFactory.Create())
            {
                DbContext context = dbContextScope.DbContexts.Get<TrackerContext>();
                TEntity entityToDelete = context.Set<TEntity>().Find(id);
                Delete(entityToDelete);
                dbContextScope.SaveChanges();
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            using (var dbContextScope = dbContextScopeFactory.Create())
            {
                DbContext context = dbContextScope.DbContexts.Get<TrackerContext>();
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    context.Set<TEntity>().Attach(entityToDelete);
                }
                context.Set<TEntity>().Remove(entityToDelete);
                dbContextScope.SaveChanges();
            }
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            using (var dbContextScope = dbContextScopeFactory.Create())
            {
                DbContext context = dbContextScope.DbContexts.Get<TrackerContext>();
                context.Set<TEntity>().Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
                dbContextScope.SaveChanges();
            }
        }
    }
}
