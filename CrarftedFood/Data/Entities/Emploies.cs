using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public static class Emploies
    {
        public static void AddEmployee(string name, string email, string mobile, string password)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                Data.Employee emp = new Data.Employee
                {
                    Name = name,
                    Email = email,
                    Mobile = mobile,
                    Password = password
                };
                dc.Employees.InsertOnSubmit(emp);
                dc.SubmitChanges();
            }
            
        }
    }
}
