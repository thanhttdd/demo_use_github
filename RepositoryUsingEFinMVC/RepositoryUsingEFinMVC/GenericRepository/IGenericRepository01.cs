using System.Collections.Generic;

namespace RepositoryUsingEFinMVC.GenericRepository
{
    public interface IGenericRepository01<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
    }
}
