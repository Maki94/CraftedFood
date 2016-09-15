using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using CrarftedFood.Models;
using Data.DTOs;
using Data.Entities;

namespace CrarftedFood.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            var order = new OrderViewModel
            {
                Orders = Reports.GetOrdersOfEmployee(UserSession.GetUser().EmployeeId)
            };
            return View(order);
        }
        
        #region CRUD

        [HttpPost]
        public ActionResult NewOrder(List<AddOrderModel> models)
        {
            foreach (AddOrderModel m in models)
            {
                Data.Entities.Meals.OrderMeal(m.MealId, m.EmployeeId, m.DateRequested, m.DateToDeliver, m.Note,
                    m.Quantity);
            }
            return View();
        }

        [HttpPost]
        public ActionResult CancelOrder(int orderId)
        {

            if (!Meals.CancelOrder(orderId))
                return Json(new { success = false});

            var order = new OrderViewModel
            {
                Orders = Reports.GetOrdersOfEmployee(UserSession.GetUser().EmployeeId)
            };

            return Json(new { success = true, message = Json(order.Orders)});
        }

        #endregion

        [HttpPost]
        public ActionResult GetOrders(string orderType) // order type moze da bude "mealTitle" || "quantity" || "price" || "note"
        {
            var order = new OrderViewModel
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
    }
}