using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.DAL;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class VendorController : Controller
    {
        VendorGateway vendorGateway = new VendorGateway();

        // GET: Vendor
        public ActionResult Index()
        {
            return View(vendorGateway.SelectAll());
        }

        // GET: Vendor/Details/5
        public ActionResult Details(int id)
        {
            return View(vendorGateway.Select(id));
        }

        // GET: Vendor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vendor/Create
        [HttpPost]
        public ActionResult Create(Vendor vendor)
        {
            try
            {
                // TODO: Add insert logic here
                vendorGateway.Insert(vendor);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vendor/Edit/5
        public ActionResult Edit(int id)
        {
            return View(vendorGateway.Select(id));
        }

        // POST: Vendor/Edit/5
        [HttpPost]
        public ActionResult Edit(Vendor vendor)
        {
            try
            {
                // TODO: Add update logic here
                vendorGateway.Update(vendor);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vendor/Delete/5
        public ActionResult Delete(int id)
        {
            return View(vendorGateway.Select(id));
        }

        // POST: Vendor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                vendorGateway.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
