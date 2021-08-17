using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "");

        TEntity GetByID(object id);

        void Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        bool Exists(TEntity entityToCheck);
        void Update(TEntity entityToUpdate);

        TEntity GetDBValues(TEntity localEntity);
    }
}
