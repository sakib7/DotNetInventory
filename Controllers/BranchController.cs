using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.DAL;
using Inventory.Models;

namespace Inventory.Controllers
{
    
    public class BranchController : Controller
    {
        BranchGateway gateway = new BranchGateway();
        
        // GET: Warehouse
        public ActionResult Index()
        {
            return View(gateway.SelectAll());
        }

        // GET: Warehouse/Details/5
        public ActionResult Details(int id)
        {
            return View(gateway.Select(id));
        }

        // GET: Warehouse/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/Create
        [HttpPost]
        public ActionResult Create(Branch branch)
        {
            //try
            //{
                // TODO: Add insert logic here
                User user = Session["user"] as User;
                branch.orgID = user.orgID;
                gateway.Insert(branch);
                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Warehouse/Edit/5
        public ActionResult Edit(int id)
        {
            return View(gateway.Select(id));
        }

        // POST: Warehouse/Edit/5
        [HttpPost]
        public ActionResult Edit(Branch branch)
        {
            try
            {
                // TODO: Add update logic here
                int i = gateway.Update(branch);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Warehouse/Delete/5
        public ActionResult Delete(int id)
        {
            return View(gateway.Select(id));
        }

        // POST: Warehouse/Delete/5
        [HttpPost]
        public ActionResult Delete(Branch branch)
        {
            try
            {
                // TODO: Add delete logic here
                gateway.Delete(branch.id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
