using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using WebApp1.GenericRepository;

namespace WebApp1.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable
        where TContext : DbContext, new()
    {
        private readonly TContext _context;
        private bool _disposed;
        private string _errorMsg = string.Empty;
        private DbContextTransaction _objTran;
        private Dictionary<string, object> _repositories;
        
        /// <summary>
        /// 
        /// </summary>
        public UnitOfWork()
        {
            this._context = new TContext();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed) {
                if (disposing) {
                    this._context.Dispose();
                }
            }
            this._disposed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public TContext Context
        {
            get { return this._context; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateTransaction()
        {
            this._objTran = this._context.Database.BeginTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            this._objTran.Commit();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Rollback()
        {
            this._objTran.Rollback();
            this._objTran.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            try
            {
                this._context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        this._errorMsg += string.Format("Property: {0} error {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(this._errorMsg, dbEx);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public GenericRepository<T> GenericRepository<T>() where T : class
        {
            if (this._repositories == null)
                this._repositories = new Dictionary<string, object>();

            var type = typeof(T).Name;
            if (!this._repositories.ContainsKey(type)) {
                var repositoryType = typeof(GenericRepository<T>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), this._context);
                this._repositories.Add(type, repositoryInstance);
            }
            return (GenericRepository<T>)this._repositories[type];
        }
    }
}