using System;
using EmployeeDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueryBuilder.Controllers;

namespace MyUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //add unit test

            EmployeesController pc = new EmployeesController();

            Employee emp = pc.Get(3);

            Assert.AreEqual("Ben", emp.FirstName);
        }
    }
}
