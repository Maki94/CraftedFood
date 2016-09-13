using Data;
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

        public static AddEmployeeViewModel Load(int empId)
        {
            Employee emp = Data.Entities.Employees.GetEmployeeEt(empId);
            return new AddEmployeeViewModel()
            {
                Name = emp.Name,
                Email = emp.Email,
                Role = (Data.Entities.Roles)emp.RoleId
            };
        }
    }

    public class ShowEmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Data.Entities.Roles Role { get; set; }

        public static ShowEmployeeViewModel Load(int empId)
        {
            Employee emp = Data.Entities.Employees.GetEmployeeEt(empId);
            return Load(emp);
        }

        public static ShowEmployeeViewModel Load(Employee emp)
        {
            return new ShowEmployeeViewModel()
            {
                Id = emp.EmployeeId,
                Name = emp.Name,
                Email = emp.Email,
                Mobile = emp.Mobile,
                Role = (Data.Entities.Roles)emp.RoleId
            };
        }
    }

    public class EditEmployeeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public static EditEmployeeViewModel Load(int empId)
        {
            Employee emp = Data.Entities.Employees.GetEmployeeEt(empId);
            return new EditEmployeeViewModel()
            {
                Id = emp.EmployeeId,
                Name = emp.Name,
                Email = emp.Email,
                Mobile = emp.Mobile
            };
        }
    }

    public class EmployeesViewModel
    {
        public List<ShowEmployeeViewModel> list { get; set; }

        public static EmployeesViewModel Load()
        {
            EmployeesViewModel model = new EmployeesViewModel();
            model.list = new List<ShowEmployeeViewModel>();
            List<Data.Employee> emps = Data.Entities.Employees.GetAllActiveEmployees();
            foreach (Employee e in emps)
            {
                model.list.Add(ShowEmployeeViewModel.Load(e));
            }
            return model;
        }
    }

}