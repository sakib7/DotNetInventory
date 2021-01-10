using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.DAL;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class ProductController : Controller
    {
        ProductGateway productGateway = new ProductGateway();
        BranchGateway branchGateway = new BranchGateway();
        CategoryGateway categoryGateway = new CategoryGateway();
        OrgProductGateway orgProductGateway = new OrgProductGateway();

        // GET: Product
        public ActionResult Index()
        {
            return View(productGateway.SelectAll());
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            Product product = new Product();
            product.branchList = branchGateway.SelectAll();
            product.categoryList = categoryGateway.SelectAll();
            product.orgProductList = orgProductGateway.SelectAll();
            return View(product);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            //try
            //{
            // TODO: Add insert logic here

            Product product = new Product();
            product.branchID = Convert.ToInt32(collection["branchID"]);
            product.orgProductID = Convert.ToInt32(collection["orgProductID"]);
            product.retailPrice = Convert.ToDouble(collection["retailPrice"]);
            productGateway.Insert(product);
            return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View(productGateway.Select(id));
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            //try
            //{
                // TODO: Add update logic here
                Product product = new Product();
                product.id = id;
                product.retailPrice = Convert.ToDouble(collection["retailPrice"]);
                productGateway.Update(product);
                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
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
