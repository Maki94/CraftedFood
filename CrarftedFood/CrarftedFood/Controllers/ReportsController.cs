using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using CrarftedFood.Models;
using Data.DTOs;
using Data.Entities;
using Rotativa.Options;
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
        public ActionResult Index(bool delivery = false, bool order = false, bool invoice = false)
        {
            return View();
        }

        public ActionResult Invoice(string fileName, int startDay = -1, int startMonth = -1, int startYear = -1, int endDay = -1, int endMonth = -1, int endYear = -1)
        {
            DateTime startTime, endTime;
                
            //startTime = new DateTime(startYear, startMonth, startDay);
            //endTime = new DateTime(endDay, endMonth, endDay);
            //if (startTime > endTime)L
            //{
            //    var t = startTime;
            //    startTime = endTime;
            //    endTime = t;
            //}

            // NOTE: temporary 


            startTime = new DateTime(2000, 1,1);
            endTime = new DateTime(2017, 1,1);

            var order = new OrderViewModel
            {
                Orders = Reports.GetInvoiceReport(startTime, endTime)
            };
            
            var PDF = new Rotativa.ViewAsPdf("Invoice", order)
            {
                PageSize = Size.A4
            };
            byte[] bytePDF = PDF.BuildPdf(this.ControllerContext);
            var url = System.Web.HttpContext.Current.Server.MapPath("~") + "/RESOURCES/" +
                      fileName + ".pdf";
            var fileStream = new FileStream(url, FileMode.Create, FileAccess.Write);
            fileStream.Write(bytePDF, 0, bytePDF.Length);
            fileStream.Close();
            return View(order);
        }

        private string ViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new System.IO.StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }




    }
}