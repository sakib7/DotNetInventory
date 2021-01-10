using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Models;
using Inventory.DAL;

namespace Inventory.Controllers
{
    public class AdminPurchaseController : Controller
    {
        AdminPurchaseGateway adminPurchaseGateway = new AdminPurchaseGateway();
        VendorGateway vendorGateway = new VendorGateway();
        
        // GET: AdminPurchase
        public ActionResult Index()
        {
            return View(adminPurchaseGateway.SelectAll());
        }

        // GET: AdminPurchase/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminPurchase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminPurchase/Create
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

        // GET: AdminPurchase/Edit/5
        public ActionResult Edit(int id)
        {
            Purchase purchase = adminPurchaseGateway.Select(id);
            purchase.vendorList = vendorGateway.SelectAll();
            return View(purchase);
        }

        // POST: AdminPurchase/Edit/5
        [HttpPost]
        public ActionResult Edit(Purchase purchase)
        {
            //try
            //{
            // TODO: Add update logic here
            adminPurchaseGateway.Update(purchase);

            return RedirectToAction("Index");

            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: AdminPurchase/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminPurchase/Delete/5
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
