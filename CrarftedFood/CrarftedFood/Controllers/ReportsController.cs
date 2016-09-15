using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrarftedFood.Models;
using Data.DTOs;
using Data.Entities;

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

        public ActionResult Invoice(int startDay = -1, int startMonth = -1, int startYear = -1, int endDay = -1, int endMonth = -1, int endYear = -1)
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

            return View(order);
        }




    }
}