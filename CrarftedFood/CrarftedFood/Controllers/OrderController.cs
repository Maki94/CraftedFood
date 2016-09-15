using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using CrarftedFood.Models;
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
        
        #region ORDER

        [HttpPost]
        public ActionResult NewOrder()
        {

            return View();
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