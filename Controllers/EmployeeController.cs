using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.DAL;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeGateway employeeGateway = new EmployeeGateway();
        BranchGateway branchGateway = new BranchGateway();
        // GET: Employee
        public ActionResult Index()
        {
            return View(employeeGateway.SelectAll());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            Employee employee = new Employee();
            employee.branchList = branchGateway.SelectAll();
            return View(employee);
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            //try
            //{
            // TODO: Add insert logic here

            Employee employee = new Employee();
            employee.name = collection["name"].ToString();
            employee.username = collection["username"].ToString();
            employee.password = collection["password"].ToString();
            employee.branchID = Convert.ToInt32(collection["branchID"].ToString());
            employeeGateway.Insert(employee);
                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = new Employee();
            employee = employeeGateway.Select(id);
            employee.branchList = branchGateway.SelectAll();
            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            //try
            //{
            // TODO: Add update logic here

            Employee employee = new Employee();
            employee.id = id;
            employee.name = collection["name"].ToString();
            employee.username = collection["username"].ToString();
            employee.password = collection["password"].ToString();
            employee.branchID = Convert.ToInt32(collection["branchID"].ToString());
            employeeGateway.Update(employee);
            return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View(employeeGateway.Select(id));
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            //try
            //{
            // TODO: Add delete logic here
            employeeGateway.Delete(id);
                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }
    }
}
