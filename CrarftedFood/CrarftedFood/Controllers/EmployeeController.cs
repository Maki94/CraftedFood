using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CrarftedFood.Models;

namespace CrarftedFood.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(AddEmployeeViewModel model)
        {
            string pass = Membership.GeneratePassword(7, 0);
            string hashedPass = Data.Entities.HashPassword.SaltedHashPassword(pass, model.Email);
            Data.Entities.Employees.AddEmployee(model.Name, model.Email, hashedPass, model.Role);

            //TODO posalji mail radniku da je kreiran nalog i koji mu je pass

            return View();
        }
    }
}