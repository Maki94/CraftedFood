
﻿using System;
﻿using CrarftedFood.Extentions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrarftedFood.Tests
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        //public void TestMethod1()
        //{
        //    Meals.AddMeal("Riblja Corba", "Ukusna riblja corba sa lukom", null, 342, 1, Units.mililiters, Categories.cookedMeal);
        //    //Employees.AddEmployee("Masa Djordjevic", "masa@gmail.com", "0648096042", "masa", Roles.Admin);
        //}


        //[TestMethod]
        //public void DeleteLookups()
        //{
        //    Lookups.DeleteAllLookups();
        //}

        //[TestMethod]
        //public void TestEditEmployee()
        //{
        //    Data.Entities.Employees.EditEmployee(1, "Marija Djordjevic", null, null, Data.Entities.Roles.Client);
        //}

        //[TestMethod]
        //public void HashMe()
        //{
        //    using (DataClassesDataContext dc = new DataClassesDataContext())
        //    {
        //        Employee masa = dc.Employees.First(a => a.Email == "masa@gmail.com");
        //        masa.Password = Data.Entities.HashPassword.SaltedHashPassword(masa.Password, masa.Email);

        //        dc.SubmitChanges();

        //    }
        //}

        //[TestMethod]
        //public void AddLookups()
        //{
        //    Lookups.DeleteAllLookups();
        //    Lookups.AddCategories();
        //    Lookups.AddRoles();
        //    Lookups.AddUnits();
        //}


        //[TestMethod]
        //public async void SendMail()
        //{
        //    CrarftedFood.Controllers.EmployeeController cont = new CrarftedFood.Controllers.EmployeeController();
        //    await cont.SendEmail("mitic.nikolca94@gmail.com", "Nikola Mitić", "blabla");
        //}
        [TestMethod]
        public void AddOrders()
        {
            Data.Entities.Meals.OrderMeal(1, 7, new DateTime(2016, 9, 13), new DateTime(2016, 9, 18), "kupus", 2);

            Data.Entities.Meals.OrderMeal(2, 7, new DateTime(2016, 9, 13), new DateTime(2016, 9, 19), "kecap", 1);
        }

        [TestMethod]
        public void EnumCreateListExtention()
        {
            var a = Data.Enums.Units.grams.CreateSelectListItem();


        }
    }
}