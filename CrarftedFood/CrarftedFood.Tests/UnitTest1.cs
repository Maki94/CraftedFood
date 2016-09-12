using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data;

namespace CrarftedFood.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Data.Entities.Employees.AddEmployee("Masa Djordjevic", "masa@gmail.com", "0648096042", "masa", Data.Entities.Roles.Admin);
        }

        [TestMethod]
        public void DeleteLookups()
        {
            Data.Entities.Lookups.DeleteAllLookups();
        }

        [TestMethod]
        public void AddLookups()
        {
            Data.Entities.Lookups.AddCategories();
            Data.Entities.Lookups.AddRoles();
            Data.Entities.Lookups.AddUnits();
        }

        
    }
}
