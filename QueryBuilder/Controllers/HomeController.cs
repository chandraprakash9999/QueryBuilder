using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeDataAccess;
using QueryBuilder.Models;
using QueryBuilder.Services;

namespace QueryBuilder.Controllers
{
    public class HomeController : Controller
    {

        private IEmpRepository<Employee> interfaceobj;
        public HomeController()
        {
            this.interfaceobj = new EmpRepository<Employee>();
        }

        

        public ActionResult Index()
        {
            

            return View("Index", from m in interfaceobj.getModel() select m);
        }

        public ActionResult Delete(int id)
        {
            Employee emp = interfaceobj.getModelByID(id);
            return View(emp);
        }

        [HttpPost]
        public ActionResult Delete(int id, Employee collection)
        {

            try
            {
                interfaceobj.deleteModel(id);
                interfaceobj.save();
                return RedirectToAction("index");

            }
            catch
            {
                return View();
            }

        }

       

        public ActionResult Details(int id)
        {


            return View(interfaceobj.getModelByID(id));
        }

        public ActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee collection)
        {

            try
            {
                interfaceobj.insertModel(collection);
                interfaceobj.save();
                return RedirectToAction("index");

            }
            catch
            {
                return View();
            }
          
        }

        public ActionResult Edit(int id)
        {
           
            return View(interfaceobj.getModelByID(id));
        }

       [HttpPost]
        public ActionResult Edit(int id,Employee collection)
        {

            try
            {
                interfaceobj.updateModel(collection);
                interfaceobj.save();
                return RedirectToAction("index");

            }
            catch
            {
                return View();
            }

        }
    }
}
