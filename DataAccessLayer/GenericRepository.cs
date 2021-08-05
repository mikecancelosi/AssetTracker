using DataAccessLayer.Strategies;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal TrackerContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(TrackerContext context)
        {           
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

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
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual bool Exists(TEntity entityToCheck)
        {
            return context.Set<TEntity>().Local.Any(e => e == entityToCheck);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            if (context.Entry(entityToUpdate).State == EntityState.Detached)
            {
                if (!Exists(entityToUpdate))
                {
                    Insert(entityToUpdate);
                }
                else
                {
                    context.Set<TEntity>().Attach(entityToUpdate);
                }
            }
            else if(context.Entry(entityToUpdate).State != EntityState.Added)
            {
                context.Entry(entityToUpdate).State = EntityState.Modified;
            }
        }

        public virtual TEntity GetDBValues(TEntity localEntity)
        {
            if (context.Entry(localEntity).State != EntityState.Added)
            {
                var dbValues = context.Entry(localEntity).GetDatabaseValues();
                if (dbValues != null)
                {
                    return dbValues.ToObject() as TEntity;
                }
            }

            return null;
        }
    }
}
