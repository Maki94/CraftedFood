using System;
using System.IO;
using System.Web.Mvc;
using CrarftedFood.Models;
using CrarftedFood.Tests;
using Data.Entities;
using Size = Rotativa.Options.Size;

namespace CrarftedFood.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //TODO permisija? delivery i allreports
        [AuthorizeUser(Permission = new[] { ((int)Data.Enums.Permissions.SeeReports), (int)Data.Enums.Permissions.SeeDeliveryReport })]
        public ActionResult Index(bool delivery = false, bool order = false, bool invoice = false)
        {
            return View();
        }

        [AuthorizeUser(Permission = new[] { (int)Data.Enums.Permissions.SeeReports })]
        public ActionResult Invoice(string fileName, int startDay = -1, int startMonth = -1, int startYear = -1, int endDay = -1, int endMonth = -1, int endYear = -1)

        {
            var startTime = new DateTime(startYear, startMonth, startDay);
            var endTime = new DateTime(endYear, endMonth, endDay);
            if (startTime > endTime)
            {
                var t = startTime;
                startTime = endTime;
                endTime = t;
            }

            //NOTE: temporary 


            //startTime = new DateTime(2000, 1,1);
            //endTime = new DateTime(2017, 1,1);

            var order = new ReportViewModel
            {
                Orders = Reports.GetInvoiceReport(startTime, endTime)
            };

            CreatePdf(fileName, order);

            return View(order);
        }

        [AuthorizeUser(Permission = new[] { (int)Data.Enums.Permissions.SeeReports, (int)Data.Enums.Permissions.SeeDeliveryReport })]
        public ActionResult Delivery(string fileName, int startDay = -1, int startMonth = -1, int startYear = -1)
        {
            DateTime date = new DateTime(startYear, startMonth, startDay);

            var delivery = new ReportViewModel
            {
                //TODO ovde treba da se prikaze samo ime i kolicina
                Orders = Reports.GetDeliveryReport(date)
            };

            CreatePdf(fileName, delivery);

            return View(delivery);
        }

        [AuthorizeUser(Permission = new[] { (int)Data.Enums.Permissions.SeeReports })]
        public ActionResult Orders(string fileName, int startDay = -1, int startMonth = -1, int startYear = -1)
        {
            DateTime date = new DateTime(startYear, startMonth, startDay);
            
            var order = new ReportViewModel()
            {//TODO ovo treba da se drugacije prikazuje, treba da se grupise po emp, lista je sortirana po empId, tako da bi mogo da prikazes ime emp (takodje ga imas u Dto) pa onda ispod samo njegove narudzbine ...
                Orders = Reports.GetOrderReport(date)
            };

            CreatePdf(fileName, order);

            return View(order);
        }

        private void CreatePdf(string fileName, object model)
        {
            var pdf = new Rotativa.ViewAsPdf("Invoice", model)
            {
                PageSize = Size.A4
            };
            byte[] bytePdf = pdf.BuildPdf(this.ControllerContext);
            var url = System.Web.HttpContext.Current.Server.MapPath("~") + "/RESOURCES/" +
                      fileName + ".pdf";
            var fileStream = new FileStream(url, FileMode.Create, FileAccess.Write);
            fileStream.Write(bytePdf, 0, bytePdf.Length);
            fileStream.Close();
        }
    }
}