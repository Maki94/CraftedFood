using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using CrarftedFood.Models;
using CrarftedFood.Tests;
using Data.DTOs;
using Data.Entities;

namespace CrarftedFood.Controllers
{
    public class OrderController : Controller
    {
        #region MY ORDERS

        [AuthorizeUser(Permission = new int[] { ((int)Data.Enums.Permissions.SeePersonalOrders) })]
        public ActionResult Index()
        {
            try
            {
                var order = new ReportViewModel()
                {
                    Orders = Reports.GetOrdersOfEmployee(UserSession.GetUser().EmployeeId)
                };
                return View(order);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion

        #region CRUD

        [AuthorizeUser(Permission = new int[] { ((int)Data.Enums.Permissions.OrderMeal)})]
        [HttpPost]
        public ActionResult NewOrder(List<AddOrderModel> models)
        {
            try
            {
                foreach (AddOrderModel m in models)
                {
                    Data.Entities.Meals.OrderMeal(m.MealId, m.EmployeeId, m.DateRequested, m.DateToDeliver, m.Note,
                        m.Quantity);
                }
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        [AuthorizeUser(Permission = new int[] { ((int)Data.Enums.Permissions.OrderMeal)})]
        [HttpPost]
        public ActionResult CancelOrder(int orderId)
        {

            try
            {
                if (!Meals.CancelOrder(orderId))
                    return Json(new { success = false });

                var order = new ReportViewModel
                {
                    Orders = Reports.GetOrdersOfEmployee(UserSession.GetUser().EmployeeId)
                };

                return Json(new { success = true, message = Json(order.Orders) });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion

        #region GET ORDERS

        [AuthorizeUser(Permission = new int[] { ((int)Data.Enums.Permissions.SeePersonalOrders) })]
        [HttpPost]
        public ActionResult GetOrders(string orderType) // order type moze da bude "mealTitle" || "quantity" || "price" || "note"
        {
            try
            {
                var order = new ReportViewModel()
                {
                    Orders = Reports.GetOrdersOfEmployee(UserSession.GetUser().EmployeeId)
                };

                switch (orderType)
                {
                    case "mealTitle":
                        return Json(new { success = true, message = Json(order.Orders.OrderBy(x => x.MealTitle)) });
                    case "quantity":
                        return Json(new { success = true, message = Json(order.Orders.OrderBy(x => x.Quantity)) });
                    case "price":
                        return Json(new { success = true, message = Json(order.Orders.OrderBy(x => x.Price)) });
                    case "note":
                        return Json(new { success = true, message = Json(order.Orders.OrderBy(x => x.Note)) });
                    default:
                        return Json(new { success = true, message = Json(order.Orders) });
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion

        #region COMMENT

        [AuthorizeUser(Permission = new int[] { ((int)Data.Enums.Permissions.SeePersonalOrders) })]
        [HttpPost]
        public ActionResult CommentDelivery(int requestId, string comment)
        {
            try
            {
                if (String.IsNullOrEmpty(comment))
                {
                    return Json(new { success = false, message = "incorrect parameters" });
                }

                Data.DTOs.LoginDto emp = UserSession.GetUser();
                Data.Entities.Meals.CommentRequest(requestId, emp.EmployeeId, comment);

                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Login");
            }
        }

        #endregion
    }
}