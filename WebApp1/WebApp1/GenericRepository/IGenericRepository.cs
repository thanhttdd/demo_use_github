using System.Collections.Generic;

namespace WebApp1.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object inId);
        void Insert(T obj);
        void Update(T obj);
        void Del(T obj);
    }
}
