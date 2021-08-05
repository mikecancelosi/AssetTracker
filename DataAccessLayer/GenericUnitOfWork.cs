using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class GenericUnitOfWork : IDisposable
    {
        private readonly TrackerContext context;
        public GenericUnitOfWork(TrackerContext trackerContext)
        {
            context = trackerContext;
        }

        public GenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(context);
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Rollback()
        {
            context.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }
        
        public void Dispose()
        {
            context?.Dispose();
        }

        public bool HasChanges => context.ChangeTracker.HasChanges();
    }
}
