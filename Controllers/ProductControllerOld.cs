using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.DAL;
using Inventory.Models;
using Inventory.Authorize;

namespace Inventory.Controllers
{
    [CustomAuthorize("admin")]
    public class ProductControllerOld : Controller
    {
        ProductGatewayOld productGateway = new ProductGatewayOld();
        
        // GET: Product
        public ActionResult Index()
        {
            return View(productGateway.SelectAll());
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View(productGateway.Select(id));
        }

        //a comment
        // GET: Product/Create
        public ActionResult Create()
        {
            Product product = new Product();
            CategoryGateway categoryGateway = new CategoryGateway();
            BranchGateway warehouseGateway = new BranchGateway();
            //product.categoryList = categoryGateway.SelectAll();
            //product.warehouseList = warehouseGateway.SelectAll();
            return View(product);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                // TODO: Add insert logic here
                productGateway.Insert(product);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = productGateway.Select(id);
            CategoryGateway categoryGateway = new CategoryGateway();
            BranchGateway warehouseGateway = new BranchGateway();
            //product.categoryList = categoryGateway.SelectAll();
            //product.warehouseList = warehouseGateway.SelectAll();
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                // TODO: Add update logic here
                productGateway.Update(product);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
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
