using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrarftedFood.Models
{
    public class AddEmployeeViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Data.Entities.Roles Role { get; set; }
    }
}