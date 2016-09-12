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
            Data.Entities.Emploies.AddEmployee("Masa Djordjevic", "masa@gmail.com", "0648096042", "masa");

        }
    }
}
