using Quipu.Core.DAL;

namespace Quipu.Core.BLL
{
    public interface IModelService<T> 
    {
        public bool Post(T instance, QContext context);

        public bool Update(T instance, QContext context);

        public bool Delete(T instance, QContext context);

        public bool Delete(int id, QContext context);
    }
}
