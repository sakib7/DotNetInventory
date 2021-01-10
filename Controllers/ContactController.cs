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
    public class ContactController : Controller
    {
        ContactGateway contactGateway = new ContactGateway();
        
        // GET: Contact
        public ActionResult Index()
        {
            return View(contactGateway.SelectAll());
        }

        // GET: Contact/Details/5
        public ActionResult Details(int id)
        {
            return View(contactGateway.Select(id));
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contact/Create
        [HttpPost]
        public ActionResult Create(Contact contact)
        {
            try
            {
                // TODO: Add insert logic here
                contactGateway.Insert(contact);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int id)
        {
            return View(contactGateway.Select(id));
        }

        // POST: Contact/Edit/5
        [HttpPost]
        public ActionResult Edit(Contact contact)
        {
            try
            {
                // TODO: Add update logic here
                contactGateway.Update(contact);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int id)
        {
            return View(contactGateway.Select(id));
        }

        // POST: Contact/Delete/5
        [HttpPost]
        public ActionResult Delete(Contact contact)
        {
            try
            {
                // TODO: Add delete logic here
                contactGateway.Delete(contact.id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
