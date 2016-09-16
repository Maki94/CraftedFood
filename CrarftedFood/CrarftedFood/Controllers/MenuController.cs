using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrarftedFood.Models;
using Data;
using Data.DTOs;

namespace CrarftedFood.Controllers
{
    public class MenuController : Controller
    {
        #region MENU

        public ActionResult Index()
        {
            MenuViewModel menu = new MenuViewModel();
            menu.menu = Data.Entities.Meals.GetMenu();
            return View(menu);
        }

        #endregion

        #region COMMENT

        [HttpPost]
        public ActionResult CommentMeal(int mealId, string comment)
        {
            if (mealId == null || String.IsNullOrEmpty(comment))
            {
                return Json(new { success = false, message = "incorrect parameters" });
            }

            Employee emp = UserSession.GetUser();
            Data.Entities.Meals.CommentMeal(emp.EmployeeId, mealId, comment);

            return Json(new { success = true });

        }


        [HttpPost]
        public ActionResult GetComments(int mealId)
        {
            var cmms = Data.Entities.Meals.GetComments(mealId);
            return Json(new { success = true , comments = cmms });
        }

        #endregion

        #region RATE
        
        [HttpPost]
        public ActionResult RateMeal(int mealId, float rating)
        {
            if (mealId == null || rating == null)
            {
                return Json(new { success = false, message = "incorrect parameters" });
            }

            Employee emp = UserSession.GetUser();
            Data.Entities.Meals.RateMeal(emp.EmployeeId, mealId, rating);
            float newrate = Data.Entities.Meals.GetAverageRate(mealId);
            return Json(new { success = true, newRating =  newrate});
        } 

        #endregion

        #region ADD

        public ActionResult AddMeal()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditMealImage(HttpPostedFileBase file, int mealId)
        {
            if (file != null)
            {
                byte[] array;
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    array = ms.GetBuffer();
                }
                Data.Entities.Meals.editImage(mealId, array);
            }

            return Json(new {success = true});
        }

        [HttpPost]
        public ActionResult AddMeal(MenuMealItem model)
        {
            if (model.file != null)
            {
                byte[] array;
                using (MemoryStream ms = new MemoryStream())
                {
                    model.file.InputStream.CopyTo(ms);
                    array = ms.GetBuffer();
                }
                model.Image = array;
            }
            Data.Entities.Meals.AddMeal(model.Title, model.Description, model.Image, model.Price, model.Quantity, model.Unit, model.Category);
            return RedirectToAction("Index");
        }

        #endregion

        #region EDIT

        public ActionResult EditMeal(int id)
        {
            MenuMealItem model = MenuMealItem.Load(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditMeal(MenuMealItem model)
        {
            if (model.file != null)
            {
                byte[] array;
                using (MemoryStream ms = new MemoryStream())
                {
                    model.file.InputStream.CopyTo(ms);
                    array = ms.GetBuffer();
                }
                model.Image = array;
            }
            Data.Entities.Meals.EditMeal(model.MealId, model.Title, model.Description, model.Image, model.Price,
                model.Quantity, model.Unit, model.Category);
            return RedirectToAction("Index");
        }

        #endregion

        #region DELETE
        
        public ActionResult Delete(int id)
        {
            Data.Entities.Meals.DeleteMeal(id);
            return RedirectToAction("Index");
        }

        #endregion

        #region ORDER

        [HttpPost]
        public ActionResult Order(int mealId, DateTime dateToDeliver, string note, float quantity)
        {
            Data.Entities.Meals.OrderMeal(mealId, UserSession.GetUser().EmployeeId, DateTime.Now, dateToDeliver, note, quantity);
            return Json(new {success = true});
        }

        #endregion


        #region table view

        public class JsonModel
        {
            public string HTMLString { get; set; }
            public bool NoMoreData { get; set; }
            public int min { get; set; }
            public int max { get; set; }
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult =
                ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext
                (ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        [ChildActionOnly]
        public ActionResult GetTableView(List<MenuMealItem> menu)
        {
            return PartialView(menu);
        }
        
        public ActionResult TableView()
        {
            JsonModel jsonModel = new JsonModel();

            List<MenuMealItem> menu = Data.Entities.Meals.GetMenu();


            jsonModel.HTMLString = RenderPartialViewToString("GetTableView", menu);
            return Json(jsonModel, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}