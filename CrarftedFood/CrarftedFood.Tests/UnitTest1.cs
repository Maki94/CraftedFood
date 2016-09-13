using Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrarftedFood.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Meals.AddMeal("Riblja Corba", "Ukusna riblja corba sa lukom", null, 342, 1, Units.mililiters, Categories.cookedMeal);
            //Employees.AddEmployee("Masa Djordjevic", "masa@gmail.com", "0648096042", "masa", Roles.Admin);
        }


        [TestMethod]
        public void DeleteLookups()
        {
            Lookups.DeleteAllLookups();
        }

        [TestMethod]
        public void TestEditEmployee()
        {
            Data.Entities.Employees.EditEmployee(1, "Marija Djordjevic", null, null, null, Data.Entities.Roles.Client);
        }

        [TestMethod]
        public void AddLookups()
        {
            Lookups.DeleteAllLookups();
            Lookups.AddCategories();
            Lookups.AddRoles();
            Lookups.AddUnits();
        }
    }
}