using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrarftedFood.Models;
using CrarftedFood.Tests;
using Data;
using Data.DTOs;

namespace CrarftedFood.Controllers
{
    public class MenuController : Controller
    {
        #region MENU
        //all
        public ActionResult Index()
        {
            try
            {
                MenuViewModel menu = new MenuViewModel();
                menu.menu = Data.Entities.Meals.GetMenu();
                return View(menu);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion

        #region COMMENT
        
        [AuthorizeUser(Permission = new int[] { ((int)Data.Enums.Permissions.FeedbackMeal)})]
        [HttpPost]
        public ActionResult CommentMeal(int mealId, string comment)
        {
            try
            {
                if (String.IsNullOrEmpty(comment))
                {
                    return Json(new { success = false, message = "incorrect parameters" });
                }

                Data.DTOs.LoginDto emp = UserSession.GetUser();
                Data.Entities.Meals.CommentMeal(emp.EmployeeId, mealId, comment);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }

        }

        //all
        [HttpPost]
        public ActionResult GetComments(int mealId)
        {
            try
            {
                var cmms = Data.Entities.Meals.GetComments(mealId);
                return Json(new { success = true, comments = cmms });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion

        #region RATE

        [AuthorizeUser(Permission = new int[] { ((int)Data.Enums.Permissions.FeedbackMeal)})]
        [HttpPost]
        public ActionResult RateMeal(int mealId, float rating)
        {
            try
            {
                Data.DTOs.LoginDto emp = UserSession.GetUser();
                Data.Entities.Meals.RateMeal(emp.EmployeeId, mealId, rating);
                float newrate = Data.Entities.Meals.GetAverageRate(mealId);
                return Json(new { success = true, newRating = newrate });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion

        #region ADD

        [AuthorizeUser(Permission = new int[] { ((int)Data.Enums.Permissions.ManageMeals)})]
        public ActionResult AddMeal()
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

        [AuthorizeUser(Permission = new int[] { ((int)Data.Enums.Permissions.ManageMeals)})]
        [HttpPost]
        public ActionResult EditMealImage(HttpPostedFileBase file, int mealId)
        {
            try
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

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        [AuthorizeUser(Permission = new int[] { ((int)Data.Enums.Permissions.ManageMeals)})]
        [HttpPost]
        public ActionResult AddMeal(MenuMealItem model)
        {
            try
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
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion

        #region EDIT

        [AuthorizeUser(Permission = new int[] { ((int)Data.Enums.Permissions.ManageMeals)})]
        public ActionResult EditMeal(int id)
        {
            try
            {
                MenuMealItem model = MenuMealItem.Load(id);
                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        [AuthorizeUser(Permission = new int[] { ((int)Data.Enums.Permissions.ManageMeals)})]
        [HttpPost]
        public ActionResult EditMeal(MenuMealItem model)
        {
            try
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
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion

        #region DELETE

        [AuthorizeUser(Permission = new int[] { ((int)Data.Enums.Permissions.ManageMeals) })]
        public async System.Threading.Tasks.Task<ActionResult> Delete(int id)
        {
            try
            {
                List<Data.DTOs.SendMailDto> sendMail = Data.Entities.Meals.DeleteMeal(id);
                foreach (SendMailDto sendMailDto in sendMail)
                {
                    await EmployeesController.SendEmail(sendMailDto.Email, "Automatsko otkazivanje narudžbine", sendMailDto.Message);
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion

        #region ORDER

        [AuthorizeUser(Permission = new int[] { ((int)Data.Enums.Permissions.OrderMeal)})]
        [HttpPost]
        public ActionResult Order(int mealId, DateTime dateToDeliver, string note, float quantity)
        {
            try
            {
                Data.Entities.Meals.OrderMeal(mealId, UserSession.GetUser().EmployeeId, DateTime.Now, dateToDeliver, note, quantity);
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion

        #region TABLE VIEW

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

        //all
        [ChildActionOnly]
        public ActionResult GetTableView(List<MenuMealItem> menu)
        {
            try
            {
                return PartialView(menu);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        //all
        public ActionResult TableView()
        {
            try
            {
                JsonModel jsonModel = new JsonModel();

                List<MenuMealItem> menu = Data.Entities.Meals.GetMenu();


                jsonModel.HTMLString = RenderPartialViewToString("GetTableView", menu);
                return Json(jsonModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion

    }
}