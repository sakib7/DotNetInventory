using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.DAL;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class OrgProductController : Controller
    {
        OrgProductGateway orgProductGateway = new OrgProductGateway();
        CategoryGateway categoryGateway = new CategoryGateway();
        // GET: OrgProduct
        public ActionResult Index()
        {
            return View(orgProductGateway.SelectAll());
        }

        // GET: OrgProduct/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrgProduct/Create
        public ActionResult Create()
        {
            OrgProduct orgProduct = new OrgProduct();
            orgProduct.categories = categoryGateway.SelectAll();
            orgProduct.categoryID = 0;
            return View(orgProduct);
        }

        // POST: OrgProduct/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
           
                // TODO: Add insert logic here
                OrgProduct orgProduct = new OrgProduct();
                User user = Session["user"] as User;
                orgProduct.name = collection["name"];
                orgProduct.brand = collection["brand"];
                orgProduct.orgID = user.orgID;
                orgProduct.categoryID = Convert.ToInt32(collection["categoryID"]);
                int id = orgProductGateway.Insert(orgProduct);
                return RedirectToAction("Index");
            
        }

        // GET: OrgProduct/Edit/5
        public ActionResult Edit(int id)
        {
            OrgProduct orgProduct = orgProductGateway.Select(id);
            orgProduct.categories = categoryGateway.SelectAll();
            return View(orgProduct);
        }

        // POST: OrgProduct/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            //try
            //{
                // TODO: Add update logic here
                OrgProduct orgProduct = new OrgProduct();
                orgProduct.id = id;
                orgProduct.name = collection["name"];
                orgProduct.brand = collection["brand"];
                orgProduct.categoryID = Convert.ToInt32(collection["categoryID"]);
                orgProductGateway.Update(orgProduct);
                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: OrgProduct/Delete/5
        public ActionResult Delete(int id)
        {
            return View(orgProductGateway.Select(id));
        }

        // POST: OrgProduct/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                orgProductGateway.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
