using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrarftedFood.Models;
using Data;

namespace CrarftedFood.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            Employee emp = Data.Entities.Login.CheckUsernameAndPassword(model.Email, model.Password);
            if (emp == null)
            {
                return Json(new { success = false, message = "incorrect credientals" });
            }

            UserSession.SetUser(emp);
            Session.Timeout = model.RememberMe ? 525600 : 20;


            //priveremeno
            return Json(new { success = true, message = "logged in" });

            //return Redirect((Data.Entities.Roles)emp.RoleId);
        }

        //public ActionResult Redirect(Data.Entities.Roles role)
        //{
        //    switch (role)
        //    {
        //        case Data.Entities.Roles.Admin:
        //            return RedirectToAction();
        //            break;
        //        case Data.Entities.Roles.User:
        //            return RedirectToAction();
        //            break;
        //        case Data.Entities.Roles.Client:
        //            return RedirectToAction();
        //            break;
        //    }
        //}


    }
}