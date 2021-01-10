using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.DAL;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class OrganizationController : Controller
    {
        OrganizationGateway organizationGateway = new OrganizationGateway();
        // GET: Organization
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            Organization organization = new Organization();
            organization.name = formCollection["name"];
            organization.description = formCollection["description"];
            organization.address = formCollection["address"];
            int id = organizationGateway.Insert(organization);
            User userSession = Session["user"] as User;
            userSession.orgID = id;
            Session["user"] = userSession;
            return RedirectToAction("Index");
        }
    }
}