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
            List<object> parameters = new List<object>();
            parameters.Add(model.Name);
            parameters.Add(model.Role);
            parameters.Add(model.Email);
            parameters.Add(pass);
            await SendEmail(model.Email, body, parameters);
            return View();
        }

        public async Task SendEmail(string email, string body, List<object> parameters, byte[] pdf = null)
        {
            //MemoryStream stream = new MemoryStream(pdf);
            string admin = "vatreneskoljke@gmail.com";
            string adminpass = "seashellsonfire";
            
            var message = new MailMessage();
            message.To.Add(new MailAddress(email));
            message.From = new MailAddress(admin);
            message.Subject = "Welcome";
            //message.Body = string.Format(body, name, role, email, password);
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


    }
}