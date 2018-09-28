using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using QueryBuilder.Controllers;
using EmployeeDataAccess;
using QueryBuilder.Models;
using QueryBuilder.Services;
using System.Web.Mvc;

namespace MyUnitTestProject
{
    [TestFixture]
    class NunitTest
    {
        [TestCase]
        public void TestMethod1()
        {
            //add unit test

            EmployeesController pc = new EmployeesController();

            Employee emp = pc.Get(3);

            Assert.AreEqual("Ben", emp.FirstName);
        }

        [TestCase]
        public void TestMethod2()
        {
            //add unit test

            EmployeesController pc = new EmployeesController();

            IEnumerable<Employee> emplist = pc.Get();
            int count = emplist.Count();

            Assert.AreEqual(7, count);
        }

        [TestCase]
        public void TestMethod3()
        {
            //add unit test

            EmployeesController pc = new EmployeesController();

            List<Columns> column = pc.GetColumnNames().ToList();
            var matchingvalues = column
  .Where(stringToCheck => stringToCheck.columnname.Contains("FirstName"));
           
            Assert.IsTrue(matchingvalues != null);
        }

        [TestCase]
        public void TestMethod4()
        {
            //add unit test

            EmployeesController pc = new EmployeesController();
            List<EmployeeQuery> querylist = new List<EmployeeQuery>();
            querylist.Add(new EmployeeQuery()
            {
                column = "FirstName",
                condition = "=",
                andor="",
                value="'John'"
            }
               

                );


            List<Employee> result = pc.Post(querylist).ToList();
            Assert.AreEqual(1, result.Count);
        }

        private static HomeController GetEmployeeController()

        {

            HomeController empcontroller = new HomeController();           

            return empcontroller;

        }

        [TestCase]
        public void IndexView()
        {
            var empcontroller = GetEmployeeController();

            ViewResult result = (ViewResult)empcontroller.Index();

            Assert.AreEqual("Index", result.ViewName);
        }


        [TestCase]
        public void GetAllEmployeeFromRepository()

        {

            // Arrange

            Employee employee1 = GetEmployeeName("Mark", "Hastings", "M", 60000);

            Employee employee2 = GetEmployeeName("Steve", "Pound", "M", 45000);

            EmpRepository<Employee> emprepository = new EmpRepository<Employee>(); 

            emprepository.insertModel(employee1);

            emprepository.insertModel(employee2);

            var controller = GetEmployeeController();

            var result = (ViewResult)controller.Index();

            var datamodel = (IEnumerable<Employee>)result.ViewData.Model;
            var list = datamodel.ToList();
            
            CollectionAssert.Contains(datamodel, employee1);

            CollectionAssert.Contains(datamodel.ToList(), employee2);

        }



        Employee GetEmployeeName(string FirstName, string LastName, string Gender, int Salary)

        {

            return new Employee

            {
                
                FirstName=FirstName,
                LastName=LastName,
                Gender=Gender,
                Salary=Salary

            };

        }

        [TestCase]
        public void Create_PostEmployeeInRepository()

        {

            EmpRepository<Employee> emprepository = new EmpRepository<Employee>();

            HomeController empcontroller = GetEmployeeController();

            Employee employee = GetEmployeeID();

            empcontroller.Create(employee);

            IEnumerable<Employee> employees = emprepository.getModel();

            Assert.IsTrue(employees.Contains(employee));

        }



        /// <summary>

        ///

        /// </summary>

        /// <returns></returns>

        Employee GetEmployeeID()

        {

            return GetEmployeeName("Prakash", "Chandra", "M", 18000);

        }



    }
}
