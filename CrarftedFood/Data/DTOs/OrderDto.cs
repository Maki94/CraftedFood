using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class OrderDto
    {
        public int EmployeeId { get; set; }
        public int OrderId { get; set; }
        public string EmployeeName { get; set; }
        
        public string MealTitle { get; set; }
        public double Quantity { get; set; }
        public string Note { get; set; }
        public double Price { get; set; }

        public double TotalPrice { get; set; }
    }
}
