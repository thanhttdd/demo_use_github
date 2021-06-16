using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using WebApp1.Models;
using WebApp1.UnitOfWork;

namespace WebApp1.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private IDbSet<T> _entities;
        private string _errorMsg = string.Empty;
        private bool _isDisposed;

        public GenericRepository(IUnitOfWork<dbContext> unitOfWork) : this(unitOfWork.Context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inContext"></param>
        public GenericRepository(dbContext inContext)
        {
            this._isDisposed = false;
            this.Context = inContext;
        }

        public dbContext Context { get; set; }

        public virtual IQueryable<T> Table
        {
            get { return this.Entities; }
        }

        protected virtual IDbSet<T> Entities
        {
            get { return this._entities ?? (this._entities = Context.Set<T>()); }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (this.Context != null)
                this.Context.Dispose();
            this._isDisposed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return this.Entities.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inId"></param>
        /// <returns></returns>
        public virtual T GetById(object inId)
        {
            return this.Entities.Find(inId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Insert(T obj)
        {
            try
            {
                if (obj == null)
                    throw new ArgumentNullException("obj");

                this.Entities.Add(obj);
                if (this.Context == null || this._isDisposed)
                    this.Context = new dbContext();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        this._errorMsg += string.Format("Property: {0} Error {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(this._errorMsg, dbEx);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inEntities"></param>
        public void BulkInsert(IEnumerable<T> inEntities)
        {
            try
            {
                if (inEntities == null)
                    throw new ArgumentNullException("inEntities");

                this.Context.Configuration.AutoDetectChangesEnabled = false;
                this.Context.Set<T>().AddRange(inEntities);
                this.Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        this._errorMsg += string.Format("Property: {0} Error {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(this._errorMsg, dbEx);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Update(T obj)
        {
            try
            {
                if(obj == null)
                    throw new ArgumentNullException("obj");

                if (this.Context == null || this._isDisposed)
                    this.Context = new dbContext();

                SetEntryModified(obj);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        this._errorMsg += string.Format("Property: {0} Error {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(this._errorMsg, dbEx);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public virtual void Del(T obj)
        {
            try
            {
                if(obj == null)
                    throw new ArgumentNullException("obj");
                if (this.Context == null || this._isDisposed)
                    this.Context = new dbContext();

                this.Entities.Remove(obj);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        this._errorMsg += string.Format("Property: {0} Error {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(this._errorMsg, dbEx);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public virtual void SetEntryModified(T obj)
        {
            this.Context.Entry(obj).State = EntityState.Modified;
        }
    }
}