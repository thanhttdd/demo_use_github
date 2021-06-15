using System.Collections.Generic;
using System.Data.Entity;

namespace RepositoryUsingEFinMVC.UnitOfWork
{
    public interface IUnitOfWork01<out TContext>
        where TContext : DbContext, new()
    {
        TContext Context { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}
