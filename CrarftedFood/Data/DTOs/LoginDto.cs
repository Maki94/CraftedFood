using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTOs
{
    public class LoginDto
    {
        public int EmployeeId { get; set; }
        public String Name { get; set; }
        public String Mobile { get; set; }
        public String Email { get; set; }
        public List<int> Permissions { get; set; }
        public int RoleId { get; set; }
    }
}
