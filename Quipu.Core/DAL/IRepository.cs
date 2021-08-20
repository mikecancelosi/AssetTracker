using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quipu.Core.DAL
{
    public interface IRepository<T>
    {
        public Task<T> Create(T instance);

        public Task Delete(int id);

        public Task<IEnumerable<T>> Get();

        public Task Update(T instance);       

    }
}
