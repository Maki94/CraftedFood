using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CrarftedFood.Models;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Web.Helpers;
using System.Web.Management;
using CrarftedFood.Tests;
using Data;
using Data.Entities;
using Data.Enums;
using Microsoft.Ajax.Utilities;
using Rotativa;

namespace CrarftedFood.Controllers
{
    public class EmployeesController : Controller
    {
        #region LIST OF EMPLOYEES AND PROFILES

        [AuthorizeUser(Permission = new int[] {((int) Permissions.ManageEmployees)})]
        public ActionResult Index()
        {
            EmployeesViewModel model = EmployeesViewModel.Load();
            return View(model);
        }

        [AuthorizeUser(Permission = new int[] {((int) Permissions.ManageEmployees), ((int) Permissions.EditProfile)})]
        public ActionResult Profile(int id)
        {
            if (UserSession.CheckUserID(id) || UserSession.IfAdmin())
            {
                Data.DTOs.EmployeeDto model = Data.DTOs.EmployeeDto.Load(id);
                return View(model);
            }
            return RedirectToAction("Unauthorized", "Login");
        }

        #endregion

        #region ADD

        [AuthorizeUser(Permission = new int[] {((int) Data.Enums.Permissions.ManageEmployees)})]
        public ActionResult AddEmployee()
        {
            return View();
        }

        [AuthorizeUser(Permission = new int[] {((int) Data.Enums.Permissions.ManageEmployees)})]
        [HttpPost]
        public async Task<ActionResult> AddEmployee(Data.DTOs.EmployeeDto model)
        {
            string pass = Membership.GeneratePassword(7, 0);
            string hashedPass = Data.Entities.HashPassword.SaltedHashPassword(pass, model.Email);
            Data.Entities.Employees.AddEmployee(model.Name, model.Email, hashedPass, model.Role);

            string body =
                "<p>Poštovani {0},</p> <p> Upravo ste dodati u bazu Crafted Food radi lakšeg naručivanja hrane kao <strong>{1}</strong>, Vaši podaci za logovanje su: <br> username: {2} <br>  password: <font color=blue>{3}</p><p>Srdačno, <br>Vatrene školjke</p>";
            string message = string.Format(body, model.Name, model.Role, model.Email, pass);
            await SendEmail(model.Email, "Welcome to Craft Food", message);
            return View();
        }

        #endregion

        #region PASSWORD RECOVERY

        //all
        [HttpPost]
        public async Task<ActionResult> PasswordRecovery(string email)
        {
            if (Data.Entities.Employees.CheckEmail(email))
            {
                List<object> obj = Data.Entities.Employees.PasswordRecovery(email);
                if (obj.Any())
                {
                    string body =
                        "<p>Poštovani {0},</p> <p> Vaša šifra je restartovana, Vaši novi podaci za logovanje su: <br> username: {1} <br>  password: <font color=blue>{2}</p><p>Srdačno, <br>Vatrene školjke</p>";
                    string message = string.Format(body, obj[0], obj[1], obj[2]);
                    await SendEmail(email, "Password Recovery", message);

                    return Json(new {success = true, message = "recovered"});
                }
            }

            return Json(new {success = false, message = "deleted"});
        }

        //all
        public static async Task SendEmail(string email, string title, string body, byte[] pdf = null)
        {
            //MemoryStream stream = new MemoryStream(pdf);
            string admin = "vatreneskoljke@gmail.com";
            string adminpass = "seashellsonfire";

            var message = new MailMessage();
            message.To.Add(new MailAddress(email));
            message.From = new MailAddress(admin);
            message.Subject = title;
            message.Body = body;
            message.IsBodyHtml = true;
            //message.Attachments.Add(new Attachment(stream, "Request.pdf", System.Net.Mime.MediaTypeNames.Application.Pdf));


            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = admin,
                    Password = adminpass
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }

        #endregion

        #region ADMIN EDIT

        [AuthorizeUser(Permission = new int[] {((int) Data.Enums.Permissions.ManageEmployees)})]
        public ActionResult EditEmployee(int empId)
        {
            Data.DTOs.EmployeeDto model = Data.DTOs.EmployeeDto.Load(empId);
            //TODO view
            return View(model);
        }

        [AuthorizeUser(Permission = new int[] {((int) Data.Enums.Permissions.ManageEmployees)})]
        [HttpPost]
        public ActionResult EditEmployee(Data.DTOs.EmployeeDto model)
        {
            Data.Entities.Employees.EditEmployee(model.EmployeeId, model.Name, model.Email, model.Mobile, model.Role);
            return RedirectToAction("Profile", model.EmployeeId);
        }

        #endregion

        #region EDIT

        [AuthorizeUser(Permission = new int[] {((int) Permissions.ManageEmployees), ((int) Permissions.EditProfile)})]
        public ActionResult EditProfile(int empId)
        {
            if (UserSession.CheckUserID(empId) || UserSession.IfAdmin())
            {
                Data.DTOs.EmployeeDto model = Data.DTOs.EmployeeDto.Load(empId);
                return View(model);
            }
            return RedirectToAction("Unauthorized", "Login");
        }


        [AuthorizeUser(Permission = new int[] {((int) Permissions.ManageEmployees), ((int) Permissions.EditProfile)})]
        [HttpPost]
        public ActionResult Profile(Data.DTOs.EmployeeDto model)
        {
            if (UserSession.CheckUserID(model.EmployeeId) || UserSession.IfAdmin())
            {
                Data.Entities.Employees.EditEmployee(model.EmployeeId, model.Name, model.Email, model.Mobile, model.Role);
                return RedirectToAction("Index");

            }
            return RedirectToAction("Unauthorized", "Login");
        }

        #endregion

        #region DELETE

        [AuthorizeUser(Permission = new int[] {((int) Data.Enums.Permissions.ManageEmployees)})]
        [HttpPost]
        public ActionResult DeleteEmployee(int id)
        {
            Data.Entities.Employees.DeleteEmployee(id);
            return RedirectToAction("Index");
        }

        #endregion

        [HttpPost]
        public ActionResult CheckPassword(string pass)
        {
            bool state = Login.CheckUsernameAndPassword(UserSession.GetUser().Email, pass) != null;
            return Json(new {success = state});
        }

        //all
        [HttpPost]
        public ActionResult ChangePassword(int id, string password, string oldPassowrd)
        {
            try
            {
                if (Login.CheckUsernameAndPassword(UserSession.GetUser().Email, oldPassowrd) == null)
                    return Json(new {success = false});

                Employees.ChangePassword(id, password);
                return Json(new {success = true});
            }
            catch (Exception)
            {
                return Json(new {success = false});
            }
        }
    }
}