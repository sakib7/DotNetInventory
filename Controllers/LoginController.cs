using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.DAL;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class LoginController : Controller
    {

        UserGateway userGateway = new UserGateway();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            string username = formCollection["username"];
            string pass = formCollection["password"];
            User user = userGateway.Auth(username,pass);
            if(user != null)
            {
                
                if(user.role.Equals("admin"))
                {
                    Session["user"] = userGateway.SelectAdmin(username,pass);
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    Session["user"] = userGateway.SelectEmployee(username, pass);
                    return RedirectToAction("Index", "EmployeeProduct");
                }
            }
            return Content("Username and password doesn't match");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}