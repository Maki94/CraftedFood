using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;

namespace CrarftedFood.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        public class CommentBindClass
        {
            public int? mealId;
            public string comment;
        }

        [HttpPost]
        public ActionResult CommentMeal(CommentBindClass model)
        {
            if (model == null || model.mealId == null || String.IsNullOrEmpty(model?.comment))
            {
                return Json(new { success = false, message = "incorrect parameters" });
            }

            Employee emp = UserSession.GetUser();
            Data.Entities.Meals.CommentMeal(emp.EmployeeId, model.mealId.Value, model.comment);

            return Json(new { success = true});

        }

        public class RateBindClass
        {
            public int? mealId;
            public double? rating;
        }

        [HttpPost]
        public ActionResult RateMeal(RateBindClass model)
        {
            if (model == null || model.mealId == null || model.rating == null)
            {
                return Json(new { success = false, message = "incorrect parameters" });
            }

            Employee emp = UserSession.GetUser();
            Data.Entities.Meals.RateMeal(emp.EmployeeId, model.mealId.Value, model.rating.Value);

            return Json(new { success = true });

        }

    }
}