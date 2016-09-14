using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DTOs;

namespace Data.Entities
{
    public static class Reports
    {
        public static List<OrderDto> GetOrderReport(int empId, DateTime date)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
               return dc.Requests.Where(a=> a.EmployeeId == empId && a.DateRequested.Value.Date == date.Date).Select(a => new OrderDto
                {
                    Quantity = a.Quantity,
                    Price = a.Meal.Price*a.Quantity,
                    MealTitle = a.Meal.Title,
                    Note = a.Note
                }).ToList();
            }
        }

        public static List<OrderDto> GetDeliveryReport(DateTime date)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                return dc.Requests.Where(a=> a.DateRequested.Value.Date == date.Date).Select(a => new OrderDto
                {
                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.Employee.Name,
                    Quantity = a.Quantity,
                    Price = a.Meal.Price * a.Quantity,
                    MealTitle = a.Meal.Title,
                    Note = a.Note
                }).ToList();
            }
        }

        public static List<OrderDto> GetInvoiceReport(DateTime start, DateTime end)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                //TODO razmisli sta ako nije dostavljen i slicno, razmisli o grupisanju, redosledu i slicno
                return dc.Requests.Where(a => a.DateDelivered >= start && a.DateDelivered <= end ).Select(a => new OrderDto
                {
                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.Employee.Name,
                    Quantity = a.Quantity,
                    Price = a.Meal.Price * a.Quantity,
                    MealTitle = a.Meal.Title,
                    Note = a.Note
                }).ToList();
            }
        }

        public static List<OrderDto> GetOrdersOfEmployee(int empId, DateTime start, DateTime end)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                return dc.Requests.Where(a => a.EmployeeId == empId && a.DateRequested.Value.Date <= end.Date && a.DateRequested.Value.Date >= start.Date).Select(a => new OrderDto
                {
                    Quantity = a.Quantity,
                    Price = a.Meal.Price * a.Quantity,
                    MealTitle = a.Meal.Title,
                    Note = a.Note
                }).ToList();
            }
        }

    }
}
