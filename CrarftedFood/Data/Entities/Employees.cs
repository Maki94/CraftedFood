using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using Data.DTOs;
using Roles = Data.Enums.Roles;

namespace Data.Entities
{
    public static class Employees
    {
        public static void AddEmployee(string name, string email, string password, Roles role)
        {
            using (var dc = new DataClassesDataContext())
            {
                var emp = new Employee
                {
                    Name = name,
                    Email = email,
                    Password = password,
                    RoleId = (int) role,
                    IsActive = true
                };
                dc.Employees.InsertOnSubmit(emp);
                dc.SubmitChanges();
            }
        }

        public static EmployeeDto GetEmployeeFullDto(int empId)
        {
            using (var dc = new DataClassesDataContext())
            {
                return dc.Employees.Where(x => (x.EmployeeId == empId) && x.IsActive).Select(x=> new EmployeeDto()
                {
                    Name = x.Name,
                    Mobile = x.Mobile,
                    Email = x.Email,
                    EmployeeId = x.EmployeeId,
                    Role = (Data.Enums.Roles)x.RoleId
                }).First();
            }
        }

        public static List<EmployeeDto> GetAllActiveEmployeesDto()
        {
            using (var dc = new DataClassesDataContext())
            {
                return dc.Employees.Where(x => x.IsActive).Select(x => new EmployeeDto()
                {
                    Name = x.Name,
                    Mobile = x.Mobile,
                    Email = x.Email,
                    EmployeeId = x.EmployeeId,
                    Role = (Data.Enums.Roles)x.RoleId
                }).ToList();
            }
        }

        public static EmployeeDto GetEmployeeAddDto(int empId)
        {
            using (var dc = new DataClassesDataContext())
            {
                return dc.Employees.Where(x => (x.EmployeeId == empId) && x.IsActive).Select(x => new EmployeeDto()
                {
                    Name = x.Name,
                    Email = x.Email,
                    Role = (Data.Enums.Roles)x.RoleId
                }).First();
            }
        }

        public static void EditEmployee(int empId, string name = null, string email = null, string mobile = null,
            Roles role = 0)
        {
            using (var dc = new DataClassesDataContext())
            {
                try
                {
                    var emp = dc.Employees.First(x => x.EmployeeId == empId);
                    if (emp.IsActive)
                    {
                        if (name != null)
                            emp.Name = name;
                        if (email != null)
                            emp.Email = email;
                        if (mobile != null)
                            emp.Mobile = mobile;
                        if (role != 0)
                            emp.RoleId = (int) role;
                        dc.SubmitChanges();
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Nije izmenjen");
                }
            }
        }

        public static void ChangePassword(int empId, string password)
        {
            using (var dc = new DataClassesDataContext())
            {
                try
                {
                    var emp = dc.Employees.First(x => x.EmployeeId == empId);
                    if (emp.IsActive)
                    {
                        var hashedPass = HashPassword.SaltedHashPassword(password, emp.Email);
                        emp.Name = hashedPass;
                        dc.SubmitChanges();
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Nije izmenjen");
                }
            }
        }

        public static bool CheckEmail(string email)
        {
            using (var dc = new DataClassesDataContext())
            {
                return dc.Employees.Any(x => x.Email == email);
            }
        }

        public static void EditMyData(int empId, string name = null, string email = null, string mobile = null)
        {
            using (var dc = new DataClassesDataContext())
            {
                try
                {
                    var emp = dc.Employees.First(x => x.EmployeeId == empId);

                    if (emp.IsActive)
                    {
                        if (name != null)
                            emp.Name = name;
                        if (email != null)
                            emp.Email = email;
                        if (mobile != null)
                            emp.Mobile = mobile;

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
            using (var dc = new DataClassesDataContext())
            {
                try
                {
                    var emp = dc.Employees.First(x => x.EmployeeId == empId);
                    emp.IsActive = false;
                    dc.SubmitChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static Employee GetEmployeeAt(int empId)
        {
            using (var dc = new DataClassesDataContext())
            {
                return dc.Employees.First(x => (x.EmployeeId == empId) && x.IsActive);
            }
        }
        
        public static List<Employee> GetAllActiveEmployees()
        {
            using (var dc = new DataClassesDataContext())
            {
                return dc.Employees.Where(x => x.IsActive).ToList();
            }
        }

        public static List<object> PasswordRecovery(string email)
        {
            using (var dc = new DataClassesDataContext())
            {
                try
                {
                    var ret = new List<object>();
                    var emp = dc.Employees.First(x => x.Email == email);
                    if (emp.IsActive)
                    {
                        var pass = Membership.GeneratePassword(7, 0);
                        var hashedPass = HashPassword.SaltedHashPassword(pass, emp.Email);
                        emp.Password = hashedPass;
                        dc.SubmitChanges();

                        ret.Add(emp.Name);
                        ret.Add(emp.Email);
                        ret.Add(pass);
                    }

                    return ret;
                }
                catch (Exception)
                {
                    throw new Exception("Nije izmenjen");
                }
            }
        }
    }
}