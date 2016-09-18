using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Data.DTOs;
using System.Data.Entity;

namespace Data.Entities
{
    public static class Reports
    {
        public static List<OrderDto> GetOrderReport(DateTime date)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                var order =
                    dc.Requests.Where(a => a.DateToDeliver.Date == date.Date)
                        .Include(a => a.Employee)
                        .Include(a => a.Meal)
                        .Select(a => new OrderDto
                        {
                            EmployeeName = a.Employee.Name,
                            MealTitle = a.Meal.Title,
                            Quantity = a.Quantity,
                            Note = a.Note,
                            Comment = a.Comment
                        }).ToList().OrderBy(a => a.EmployeeName).ToList();

                string previous = "";
                foreach (OrderDto orderDto in order)
                {
                    if (previous == orderDto.EmployeeName)
                    {
                        orderDto.EmployeeName = null;
                    }
                    else
                    {
                        previous = orderDto.EmployeeName;
                    }
                }

                return order;
            }
        }

        public static List<OrderDto> GetDeliveryReport(DateTime date)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                List<OrderDto> order = new List<OrderDto>();

                List<MenuMealItem> meals = Data.Entities.Meals.GetMenu();

                foreach (MenuMealItem meal in meals)
                {
                    float totalQuantity =
                        dc.Requests.Where(
                                a => a.MealId == meal.MealId && a.DateToDeliver == date)
                            .Select(a => a.Quantity)
                            .ToList()
                            .Sum();

                    if (totalQuantity != 0)
                    {
                        order.Add(new OrderDto()
                        {
                            MealTitle = meal.Title,
                            Quantity = totalQuantity
                        });
                    }
                }
                return order;
            }
        }

        public static List<OrderDto> GetInvoiceReport(DateTime start, DateTime end)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                List<OrderDto> order = new List<OrderDto>();

                List<MenuMealItem> meals = Data.Entities.Meals.GetMenu();

                foreach (MenuMealItem meal in meals)
                {
                    float totalQuantity =
                        dc.Requests.Where(
                                a => a.MealId == meal.MealId && a.DateToDeliver >= start && a.DateToDeliver <= end)
                            .Select(a => a.Quantity)
                            .ToList()
                            .Sum();

                    if (totalQuantity != 0)
                    {
                        order.Add(new OrderDto()
                        {
                            MealTitle = meal.Title,
                            Quantity = totalQuantity,
                            Price = meal.Price,
                            TotalPrice = totalQuantity*meal.Price
                        });
                    }
                }

                if (order.Any())
                {
                    double totalBill = order.Sum(x => x.TotalPrice);
                    order.Add(new OrderDto()
                    {
                        MealTitle = "SALDO",
                        TotalPrice = totalBill
                    });
                }

                return order;
            }

        }

        public static List<OrderDto> GetOrdersOfEmployee(int empId, DateTime? start = null, DateTime? end = null)
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
                return dc.Requests.Where(a => a.EmployeeId == empId && start.Value.Date <= a.DateRequested.Date && a.DateRequested.Date <= end.Value.Date)
                    .Select(a => new OrderDto
                    {
                        OrderId = a.RequestId,
                        Quantity = a.Quantity,
                        Price = a.Meal.Price * a.Quantity,
                        MealTitle = a.Meal.Title,
                        Note = a.Note,
                        Date = $"{a.DateToDeliver:MM-dd-yy}",
                        DateToDeliver = a.DateToDeliver,
                        Comment = a.Comment,
                        Delivered = a.DateDelivered != null
                    }).ToList();
            }
        }

    }
}
