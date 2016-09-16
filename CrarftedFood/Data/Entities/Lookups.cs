using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Enums;


namespace Data.Enums
{
    public enum Units
    {
        [Description("gr")]
        grams = 1,
        [Description("mil")]
        mililiters,
        [Description("x")]
        piece
    }

    public enum Categories
    {
        salad = 1,
        sandwich,
        bakery,
        pasta,
        sweet,
        drink,
        [Description("cooked meal")]
        cookedMeal
    }

    public enum Roles
    {
        Admin = 1, User, Client
    }

    public enum Permissions
    {
        ManageEmployees = 1,
        EditProfile,
        ManageMeals,
        SeePersonalOrders,
        FeedbackMeal,
        OrderMeal,
        SeeDeliveryReport,
        SeeReports
    }
}

namespace Data.Entities
{

    public class RolesPermissions
    {
        public List<List<int>> rolPerm = new List<List<int>>();
        public int NumOfRoles { get; set; }


        public static RolesPermissions Load()
        {
            RolesPermissions rP = new RolesPermissions();
            List<int> AdminPerm = new List<int> { 1,3,8 };
            List<int> UserPerm = new List<int> { 7 };
            List<int> ClientPerm = new List<int> { 2,4,5,6 };

            rP.rolPerm.Add(AdminPerm);
            rP.rolPerm.Add(UserPerm);
            rP.rolPerm.Add(ClientPerm);
            rP.NumOfRoles = 3;
            return rP;
        }
    }

    public static class Lookups
    {
        public static void AddUnits()
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                foreach (var c in Enum.GetValues(typeof(Units)))
                {
                    Unit newC = new Unit { Name = c.ToString() };
                    dc.Units.InsertOnSubmit(newC);
                }
                dc.SubmitChanges();
            }
        }

        public static void AddCategories()
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                foreach (var c in Enum.GetValues(typeof(Categories)))
                {
                    Category newC = new Category { Name = c.ToString() };
                    dc.Categories.InsertOnSubmit(newC);
                }
                dc.SubmitChanges();
            }
        }

        public static void AddRoles()
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                foreach (var c in Enum.GetValues(typeof(Roles)))
                {
                    Role newC = new Role { Name = c.ToString() };
                    dc.Roles.InsertOnSubmit(newC);
                }
                dc.SubmitChanges();
            }
        }

        public static void AddPermissions()
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                foreach (var c in Enum.GetValues(typeof(Permissions)))
                {
                    Permission newC = new Permission { Name = c.ToString() };
                    dc.Permissions.InsertOnSubmit(newC);
                }
                dc.SubmitChanges();
            }
        }

        public static void AddRolesPermisons()
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                RolesPermissions rP = RolesPermissions.Load();
                for (int i = 0; i < rP.NumOfRoles; i++)
                {
                    foreach (int j in rP.rolPerm[i])
                    {
                        RolePermission rolesPermission = new RolePermission
                        {
                            RoleId = i + 1,
                            PermissionId = j,
                        };
                        dc.RolePermissions.InsertOnSubmit(rolesPermission);
                    }
                }
                dc.SubmitChanges();
            }
        }


        public static void DeleteAllLookups()
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                //dc.ExecuteCommand("DBCC CHECKIDENT('Unit', RESEED, 0);");
                //dc.ExecuteCommand("DBCC CHECKIDENT('Category', RESEED, 0);");
                //dc.ExecuteCommand("DBCC CHECKIDENT('Role', RESEED, 0);");
                dc.ExecuteCommand("DBCC CHECKIDENT('Permission', RESEED, 0);");
                dc.ExecuteCommand("DBCC CHECKIDENT('RolePermissions', RESEED, 0);");

                //var units = dc.Units.ToList();
                //dc.Units.DeleteAllOnSubmit(units);

                //var categories = dc.Categories.ToList();
                //dc.Categories.DeleteAllOnSubmit(categories);

                //var roles = dc.Roles.ToList();
                //dc.Roles.DeleteAllOnSubmit(roles);

                var rp = dc.RolePermissions.ToList();
                dc.RolePermissions.DeleteAllOnSubmit(rp);

                var permissions = dc.Permissions.ToList();
                dc.Permissions.DeleteAllOnSubmit(permissions);

                dc.SubmitChanges();

            }
        }
    }
}
