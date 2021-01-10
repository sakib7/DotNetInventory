using Inventory.DAL;
using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class CategoryController : Controller
    {
        CategoryGateway categoryGateway = new CategoryGateway();

        // GET: Category
        public ActionResult Index()
        {
            return View(categoryGateway.SelectAll());
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View(categoryGateway.Select(id));
        }

        

        // GET: Category/Create
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                // TODO: Add insert logic here
                User userSession = Session["user"] as User;
                category.orgID = userSession.orgID;
                categoryGateway.Insert(category);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return View();
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int id)
        {
            Category category = categoryGateway.Select(id);
            return View(category);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            try
            {
                // TODO: Add update logic here
                User userSession = Session["user"] as User;
                category.orgID = userSession.orgID;
                categoryGateway.Update(category);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int id)
        {
            return View(categoryGateway.Select(id));
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            try
            {
                // TODO: Add delete logic here
                categoryGateway.Delete(category.id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}
