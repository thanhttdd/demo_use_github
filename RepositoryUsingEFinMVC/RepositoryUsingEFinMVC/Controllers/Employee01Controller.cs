using RepositoryUsingEFinMVC.DAL;
using RepositoryUsingEFinMVC.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepositoryUsingEFinMVC.Controllers
{
    public class Employee01Controller : Controller
    {
        private readonly UnitOfWorkDemo _unitOfWork;

        public Employee01Controller()
        {
            this._unitOfWork = new UnitOfWorkDemo(new EmployeeDBContext());
        }

        // GET: Employee01
        public ActionResult Index()
        {
            return View(this._unitOfWork.EmployeeRepository.GetAll());
        }
    }
}