using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public static class Employees
    {
        public static void AddEmployee(string name, string email, string password, Roles role)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                Data.Employee emp = new Data.Employee
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    RoleId = (int)role,
                    IsActive = true
                };
                dc.Employees.InsertOnSubmit(emp);
                dc.SubmitChanges();
            }
        }

        public static void EditEmployee(int empId, string name = null, string email = null, string mobile = null, Roles role = 0)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                try
                {
                    Data.Employee emp = dc.Employees.Where(x => x.EmployeeId == empId).First();
                    if (emp.IsActive)
                    {
                        if (name != null)
                        {
                            emp.Name = name;
                        }
                        if (email != null)
                        {
                            emp.Email = email;
                        }
                        if (mobile != null)
                        {
                            emp.Mobile = mobile;
                        }
                        if (role != 0)
                        {
                            emp.RoleId = (int)role;
                        }
                        dc.SubmitChanges();
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Nije izmenjen");
                }
            }
        }

        public static void EditMyData(int empId, string name = null, string email = null, string mobile = null)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                try
                {
                    Data.Employee emp = dc.Employees.Where(x => x.EmployeeId == empId).First();

                    if (emp.IsActive)
                    {
                        if (name != null)
                        {
                            emp.Name = name;
                        }
                        if (email != null)
                        {
                            emp.Email = email;
                        }
                        if (mobile != null)
                        {
                            emp.Mobile = mobile;
                        }

                        dc.SubmitChanges(); 
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static void DeleteEmployee(int empId)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                try
                {
                    Data.Employee emp = dc.Employees.Where(x => x.EmployeeId == empId).First();
                    emp.IsActive = false;
                    dc.SubmitChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
