using RepositoryUsingEFinMVC.DAL;
using RepositoryUsingEFinMVC.GenericRepository;
using RepositoryUsingEFinMVC.Repository;
using RepositoryUsingEFinMVC.UnitOfWork;
using System;
using System.Web.Mvc;

namespace RepositoryUsingEFinMVC.Controllers
{
    public class Employee01Controller : Controller
    {
        private UnitOfWork01<EmployeeDBContext> unitOfWork = new UnitOfWork01<EmployeeDBContext>();
        private GenericRepository01<Employee> repository;
        private IEmployeeRepository01 employeeRepository;
        public Employee01Controller()
        {
            //If you want to use Generic Repository with Unit of work
            repository = new GenericRepository01<Employee>(unitOfWork);

            //If you want to use Specific Repository with Unit of work
            employeeRepository = new EmployeeRepository01(unitOfWork);
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = repository.GetAll();
            //Using Specific Repository
            //var model = employeeRepository.GetEmployeesByDepartment(1);
            return View(model);
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(Employee model)
        {
            try
            {
                unitOfWork.CreateTransaction();
                if (ModelState.IsValid)
                {
                    repository.Insert(model);
                    unitOfWork.Save();
                    //Do Some Other Task with the Database
                    //If everything is working then commit the transaction else rollback the transaction
                    unitOfWork.Commit();
                    return RedirectToAction("Index", "Employee01");
                }
            }
            catch (Exception ex)
            {
                //Log the exception and rollback the transaction
                unitOfWork.Rollback();
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditEmployee(int EmployeeId)
        {
            Employee model = repository.GetById(EmployeeId);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditEmployee(Employee model)
        {
            if (ModelState.IsValid)
            {
                repository.Update(model);
                unitOfWork.Save();
                return RedirectToAction("Index", "Employee01");
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult DeleteEmployee(int EmployeeId)
        {
            Employee model = repository.GetById(EmployeeId);
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int EmployeeID)
        {
            Employee model = repository.GetById(EmployeeID);
            repository.Delete(model);
            unitOfWork.Save();
            return RedirectToAction("Index", "Employee01");
        }
    }
}