using RepositoryUsingEFinMVC.DAL;
using RepositoryUsingEFinMVC.GenericRepository;
using RepositoryUsingEFinMVC.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryUsingEFinMVC.Repository
{
    public class EmployeeRepository01 : GenericRepository01<Employee>, IEmployeeRepository01
    {
        public EmployeeRepository01(IUnitOfWork01<EmployeeDBContext> unitOfWork)
            : base(unitOfWork)
        {
        }

        public EmployeeRepository01(EmployeeDBContext context)
            : base(context)
        {
        }

        public IEnumerable<Employee> GetEmployeesByGender(string Gender)
        {
            return Context.Employees.Where(emp => emp.Gender == Gender).ToList();
        }

        public IEnumerable<Employee> GetEmployeesByDepartment(string Dept)
        {
            return Context.Employees.Where(emp => emp.Dept == Dept).ToList();
        }
    }
}