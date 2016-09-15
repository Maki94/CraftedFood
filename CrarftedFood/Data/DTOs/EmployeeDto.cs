using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Data.Enums.Roles Role { get; set; }

        public static EmployeeDto Load(int empId)
        {
            return Data.Entities.Employees.GetEmployeeFullDto(empId);
        }
    }
}
