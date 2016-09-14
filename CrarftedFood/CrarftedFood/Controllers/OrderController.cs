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
                Orders = Reports.GetOrderDtos(UserSession.GetUser().EmployeeId)
            };
            return View(order);
        }
    }
}