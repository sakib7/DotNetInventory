using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Models;
using Inventory.DAL;

namespace Inventory.Controllers
{
    public class EmployeePurchaseController : Controller
    {
        EmployeeProductGateway employeeProductGateway = new EmployeeProductGateway();
        EmployeePurchaseGateway employeePurchaseGateway = new EmployeePurchaseGateway();
        
        // GET: EmployeePurchase
        public ActionResult Index()
        {
            return View(employeePurchaseGateway.SelectAll());
        }

        // GET: EmployeePurchase/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeePurchase/Create
        public ActionResult Create()
        {
            Purchase purchase = new Purchase();
            purchase.productList = employeeProductGateway.SelectAll();
            return View(purchase);
        }

        // POST: EmployeePurchase/Create
        [HttpPost]
        public ActionResult Create(Purchase purchase)
        {
            //try
            //{
            // TODO: Add insert logic here
            purchase.date = DateTime.Now;
            purchase.status = "requested";
            employeePurchaseGateway.Insert(purchase);
            return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: EmployeePurchase/Edit/5
        public ActionResult Edit(int id)
        {
            Purchase purchase = employeePurchaseGateway.Select(id);
            employeePurchaseGateway.Update(purchase);
            return RedirectToAction("Index");
        }

        // POST: EmployeePurchase/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeePurchase/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeePurchase/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
