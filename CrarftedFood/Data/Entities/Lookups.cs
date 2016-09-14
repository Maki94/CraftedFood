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
}

namespace Data.Entities
{

    

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



        public static void DeleteAllLookups()
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                dc.ExecuteCommand("DBCC CHECKIDENT('Unit', RESEED, 0);");
                dc.ExecuteCommand("DBCC CHECKIDENT('Category', RESEED, 0);");
                dc.ExecuteCommand("DBCC CHECKIDENT('Role', RESEED, 0);");

                var units = dc.Units.ToList();
                dc.Units.DeleteAllOnSubmit(units);

                var categories = dc.Categories.ToList();
                dc.Categories.DeleteAllOnSubmit(categories);

                var roles = dc.Roles.ToList();
                dc.Roles.DeleteAllOnSubmit(roles);

                //var rp = dc.RolePermissions.ToList();
                //dc.RolePermissions.DeleteAllOnSubmit(rp);

                //var permissions = dc.Permissions.ToList();
                //dc.Permissions.DeleteAllOnSubmit(permissions);

                dc.SubmitChanges();

            }
        }
    }
}
