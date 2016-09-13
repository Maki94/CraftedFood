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
        public async Task<ActionResult> AddEmployee(AddEmployeeViewModel model)
        {
            string pass = Membership.GeneratePassword(7, 0);
            string hashedPass = Data.Entities.HashPassword.SaltedHashPassword(pass, model.Email);
            Data.Entities.Employees.AddEmployee(model.Name, model.Email, hashedPass, model.Role);

            string body = "<p>Poštovani {0},</p> <p> Upravo ste dodati u bazu Crafted Food radi lakšeg naručivanja hrane kao <strong>{1}</strong>, Vaši podaci za logovanje su: <br> username: {2} <br>  password: <font color=blue>{3}</p><p>Pozdrav</p>";
            string message = string.Format(body, model.Name, model.Role, model.Email, pass);
            await SendEmail(model.Email, "Welcome to Craft Food", message);
            return View();
        }

        public async Task SendEmail(string email, string title, string body, byte[] pdf = null)
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
        
        
        public async Task<ActionResult> PasswordRecovery(string email)
        {
            List<object> obj = Data.Entities.Employees.PasswordRecovery(email);
            if (obj.Any())
            {
                string body = "<p>Poštovani {0},</p> <p> Vaša šifra je restartovana, Vaši novi podaci za logovanje su: <br> username: {1} <br>  password: <font color=blue>{2}</p><p>Pozdrav</p>";
                string message = string.Format(body, obj[0], obj[1], obj[2]);
                await SendEmail(email, "Password Recovery", message);

                return Json(new { success = true, message = "recovered" });
            }
            return Json(new { success = false, message = "deleted" });
        }


    }
}