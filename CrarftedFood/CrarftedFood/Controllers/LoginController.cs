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

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }


    }
}