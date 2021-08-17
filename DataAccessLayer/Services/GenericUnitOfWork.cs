using System;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace DataAccessLayer.Services
{
    public class GenericUnitOfWork : IUnitOfWork
    {
        private readonly TrackerContext context;
        public GenericUnitOfWork(TrackerContext trackerContext)
        {
            context = trackerContext;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(context);
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Rollback()
        {
            foreach(DbEntityEntry entity in context.ChangeTracker.Entries().ToList())
            {
                if(entity.State == System.Data.Entity.EntityState.Added)
                {
                    entity.State = System.Data.Entity.EntityState.Detached;
                }
                else
                {
                    entity.Reload();
                }
            }
        }
        
        public void Dispose()
        {
            context?.Dispose();
        }

        public bool HasChanges => context.ChangeTracker.HasChanges();
    }
}
