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
        #region ALL

        [AuthorizeUser(Permission = new[] { ((int)Data.Enums.Permissions.SeeReports), (int)Data.Enums.Permissions.SeeDeliveryReport })]
        public ActionResult Index(bool delivery = false, bool order = false, bool invoice = false)
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion

        #region INVOICE

        [AuthorizeUser(Permission = new[] { (int)Data.Enums.Permissions.SeeReports })]
        public ActionResult Invoice(string fileName, int startDay = -1, int startMonth = -1, int startYear = -1, int endDay = -1, int endMonth = -1, int endYear = -1)
        {
            try
            {
                var startTime = new DateTime(startYear, startMonth, startDay);
                var endTime = new DateTime(endYear, endMonth, endDay);
                if (startTime > endTime)
                {
                    var t = startTime;
                    startTime = endTime;
                    endTime = t;
                }

                var order = new ReportViewModel
                {
                    Orders = Reports.GetInvoiceReport(startTime, endTime)
                };

                CreatePdf(fileName, order, "Invoice");

                return null;
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion

        #region DELIVERY

        [AuthorizeUser(Permission = new[] { (int)Data.Enums.Permissions.SeeReports, (int)Data.Enums.Permissions.SeeDeliveryReport })]
        public ActionResult Delivery(string fileName, int startDay = -1, int startMonth = -1, int startYear = -1)
        {
            try
            {
                DateTime date = new DateTime(startYear, startMonth, startDay);

                var delivery = new ReportViewModel
                {
                    //TODO ovde treba da se prikaze samo ime i kolicina
                    Orders = Reports.GetDeliveryReport(date)
                };

                CreatePdf(fileName, delivery, "Delivery");

                return null;
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion

        #region ORDERS

        [AuthorizeUser(Permission = new[] { (int)Data.Enums.Permissions.SeeReports })]
        public ActionResult Orders(string fileName, int startDay = -1, int startMonth = -1, int startYear = -1)
        {
            try
            {
                DateTime date = new DateTime(startYear, startMonth, startDay);

                var order = new ReportViewModel()
                {//TODO ovo treba da se drugacije prikazuje, treba da se grupise po emp, lista je sortirana po empId, tako da bi mogo da prikazes ime emp (takodje ga imas u Dto) pa onda ispod samo njegove narudzbine ...
                    Orders = Reports.GetOrderReport(date)
                };

                CreatePdf(fileName, order, "Order");

                return null;
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion

        #region PDF

        private void CreatePdf(string fileName, object model, string actionName)
        {
            var pdf = new Rotativa.ViewAsPdf(actionName, model)
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

        #endregion
    }
}