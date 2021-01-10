using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.DAL;

namespace Inventory.Controllers
{
    public class RegisterController : Controller
    {

        UserGateway userGateway = new UserGateway();
        // GET: Register/Create

        public ActionResult Index()
        {
            return View();
        }

        // POST: Register/Create
        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            
            // TODO: Add insert logic here
            string name = formCollection["name"];
            string username = formCollection["username"];
            string password = formCollection["password"];

            User user = new User();
            user.name = name;
            user.username = username;
            user.password = password;

            int id = userGateway.Insert(user);

            if(id != 0)
            {
                user = userGateway.Select(id);
                Session["user"] = user;
            }

            return RedirectToAction("Index", "Organization");
            
        }

        
    }
}
