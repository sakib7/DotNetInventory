using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.DAL;

namespace Inventory.Controllers
{
    public class AdminSalesController : Controller
    {
        AdminSalesGateway adminSales = new AdminSalesGateway();
        // GET: AdminSales
        public ActionResult Index()
        {
            return View(adminSales.SelectAll());
        }
    }
}