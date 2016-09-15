using System;
using System.Collections.Generic;
using System.Linq;
using Data.DTOs;

namespace Data.Entities
{
    public static class Reports
    {
        public static List<OrderDto> GetOrderReport(int empId, DateTime date)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                return dc.Requests.Where(a => a.EmployeeId == empId && a.DateRequested.Date == date.Date).Select(a => new OrderDto
                {
                    Quantity = a.Quantity,
                    Price = a.Meal.Price * a.Quantity,
                    MealTitle = a.Meal.Title,
                    Note = a.Note
                }).ToList();
            }
        }

        public static List<OrderDto> GetDeliveryReport(DateTime date)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                return dc.Requests.Where(a => a.DateRequested.Date == date.Date).Select(a => new OrderDto
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
                return dc.Requests.Where(a => a.DateDelivered >= start && a.DateDelivered <= end).Select(a => new OrderDto
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

        public static List<OrderDto> GetOrdersOfEmployee(int empId, DateTime? start=null, DateTime? end=null)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                if (start == null)
                {
                    start = new DateTime(1753, 1, 2);
                }
                if (end == null)
                {
                    end = new DateTime(9999, 12, 31); 
                }
                return dc.Requests.Where(a => a.EmployeeId == empId && start.Value.Date<=a.DateRequested.Date && a.DateRequested.Date<=end.Value.Date)
                    .Select(a => new OrderDto
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
