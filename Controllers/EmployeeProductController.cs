﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.DAL;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class EmployeeProductController : Controller
    {
        EmployeeProductGateway employeeProductGateway = new EmployeeProductGateway();
        // GET: EmployeeProduct
        public ActionResult Index()
        {
            return View(employeeProductGateway.SelectAll());
        }

        // GET: EmployeeProduct/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeeProduct/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeProduct/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmployeeProduct/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeeProduct/Edit/5
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

        // GET: EmployeeProduct/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeeProduct/Delete/5
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
