﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        




    }
}