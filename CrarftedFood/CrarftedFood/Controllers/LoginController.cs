using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Design;
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
        public ActionResult Index(string recoverdEmail)
        {

            ViewBag.recoveredEmail = string.IsNullOrEmpty(recoverdEmail) ? "" : recoverdEmail;



            //var email = "masadordevic@gmail.com";
            //var pass = "A^>gF:@";


            
            //Data.DTOs.LoginDto emp = Data.Entities.Login.CheckUsernameAndPassword(email, pass);
            //if (emp == null)
            //{
            //    return View();
            //}
            //UserSession.SetUser(emp);
            //Session.Timeout = 525600;


            ////priveremeno
            //return RedirectToAction("Index", "Menu");


            return View();
        }

        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            Data.DTOs.LoginDto emp = Data.Entities.Login.CheckUsernameAndPassword(model.Email, model.Password);
            if (emp == null)
            {
                return View();
                return Json(new { success = false, message = "incorrect credientals" });
            }

            UserSession.SetUser(emp);
            Session.Timeout = model.RememberMe ? 525600 : 20;


            return RedirectToAction("Index","Menu");
        }

        public ActionResult Logout()
        {
            UserSession.SetUser(null);
            return RedirectToAction("Index");
        }

        //public ActionResult Redirect(Data.Enums.Roles role)
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

        public ActionResult Unauthorized()
        {
            return View();
        }

    }
}