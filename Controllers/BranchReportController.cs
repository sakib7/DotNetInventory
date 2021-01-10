using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.DAL;

namespace Inventory.Controllers
{
    public class BranchReportController : Controller
    {
        BranchReportGateway branchReport = new BranchReportGateway();
        // GET: BranchReport
        public ActionResult Index()
        {
            return View(branchReport.SelectAll());
        }
    }
}