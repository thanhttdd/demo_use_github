using RepositoryUsingEFinMVC.DAL;
using RepositoryUsingEFinMVC.GenericRepository;
using System.Collections.Generic;

namespace RepositoryUsingEFinMVC.Repository
{
    public interface IEmployeeRepository01 : IGenericRepository01<Employee>
    {
        IEnumerable<Employee> GetEmployeesByGender(string Gender);
        IEnumerable<Employee> GetEmployeesByDepartment(string Dept);
    }
}
