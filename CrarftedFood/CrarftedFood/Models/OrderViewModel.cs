using System;
using System.Collections.Generic;
using Data.DTOs;

namespace CrarftedFood.Models
{
    public class OrderViewModel
    {
        public List<OrderDto> Orders { get; set; }
    }

    public class AddOrderModel
    {
        public int MealId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DateRequested { get; set; }
        public DateTime DateToDeliver { get; set; }
        public string Note { get; set; }
        public float Quantity { get; set; }
    }
    
}