using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrarftedFood.Models
{
    public class EmployeesViewModel
    {
        public List<Data.DTOs.EmployeeDto> list { get; set; }

        public static EmployeesViewModel Load()
        {
            EmployeesViewModel model = new EmployeesViewModel();
            model.list = Data.Entities.Employees.GetAllActiveEmployeesDto();
            return model;
        }
    }

}