using RepositoryUsingEFinMVC.DAL;
using RepositoryUsingEFinMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepositoryUsingEFinMVC.UnitOfWork
{
    public class UnitOfWorkDemo
    {
        private IEmployeeRepository _employeeRepository;
        private EmployeeDBContext _dbContext;

        public UnitOfWorkDemo(EmployeeDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository();
                return _employeeRepository;
            }
        }

        public void Save()
        {
            this._dbContext.SaveChanges();
        }
    }
}