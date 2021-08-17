using DataAccessLayer.Services;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
    public class GenericTestRepository<T> : IRepository<T> where T : DatabaseBackedObject
    {
        private List<T> originalData;        

        private IQueryable<T> data;

        public GenericTestRepository(List<T> inputData)
        {
            originalData = inputData;
            data = inputData.AsQueryable();
        }

        public void Delete(object id)
        {
            T foundObject = data.FirstOrDefault(x => x.ID == (int)id);
            if(foundObject != null)
            {
                data = data.Where(x => x != foundObject);
            }

        }

        public void Delete(T entityToDelete)
        {
            data = data.Where(x => x != entityToDelete);
        }

        public bool Exists(T entityToCheck)
        {
            return data.Any(x => x == entityToCheck);
            
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, 
                                  Func<IQueryable<T>, 
                                  IOrderedQueryable<T>> orderBy = null, 
                                  string includeProperties = "")
        {
            IQueryable<T> query = data;

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

        public T GetByID(object id)
        {
           return data.FirstOrDefault(x => x.ID == (int)id);
        }

        public T GetDBValues(T localEntity)
        {
            T foundOriginalItem = originalData.FirstOrDefault(x => x.ID == localEntity.ID);
            if(foundOriginalItem != null)
            {
                return foundOriginalItem;
            }

            throw new Exception();
        }

        public void Insert(T entity)
        {
            data = data.AsEnumerable().Concat(new[] { entity }).AsQueryable();
        }

        public void Insert(IEnumerable<T> entities)
        {
            data = data.AsEnumerable().Concat(entities).AsQueryable();
        }

        public void Update(T entityToUpdate)
        {
            if (!Exists(entityToUpdate))
            {
                Insert(entityToUpdate);
            }
        }
    }
}
