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
        public ActionResult Index(string recoverdEmail)
        {


           // var email = "masadordevic@gmail.com";
           // var pass = "A^>gF:@";
           ViewBag.recoveredEmail = string.IsNullOrEmpty(recoverdEmail) ? "" : recoverdEmail;
   
           
           // Employee emp = Data.Entities.Login.CheckUsernameAndPassword(email, pass);
           // if (emp == null)
           // {
           //     return Json(new { success = false, message = "incorrect credientals" });
           // }

           // UserSession.SetUser(emp);
           // Session.Timeout = 525600;


           // //priveremeno
           // return RedirectToAction("Index", "Menu");


            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model)
        {
            Employee emp = Data.Entities.Login.CheckUsernameAndPassword(model.Email, model.Password);
            if (emp == null)
            {
                return View();
                return Json(new { success = false, message = "incorrect credientals" });
            }

            UserSession.SetUser(emp);
            Session.Timeout = model.RememberMe ? 525600 : 20;


            //priveremeno
            return Json(new { success = true, message = "logged in" });

            //return Redirect((Data.Entities.Roles)emp.RoleId);
        }

        public ActionResult Logout()
        {
            UserSession.SetUser(null);
            return RedirectToAction("Index");
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