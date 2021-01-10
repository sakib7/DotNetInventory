using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.DAL;
using Inventory.Authorize;

namespace Inventory.Controllers
{
    public class SalesOrderController : Controller
    {
        CategoryGateway categoryGateway = new CategoryGateway();
        ProductGatewayOld productGateway = new ProductGatewayOld();
        ContactGateway contactGateway = new ContactGateway();
        
        // GET: SalesOrder
        public ActionResult Index()
        {
            return View();
        }

        
        
        public ActionResult Create()
        {
            SalesOrder salesOrder = new SalesOrder();
            salesOrder.contactList = new SelectList(contactGateway.SelectAll(), "id", "name");
            salesOrder.categoryList = new SelectList(categoryGateway.SelectAll(), "id", "name");
            salesOrder.productList = new SelectList(productGateway.SelectAll(), "id", "name");

            salesOrder.contacts = contactGateway.SelectAll();
            salesOrder.categories = categoryGateway.SelectAll();
            //salesOrder.products = productGateway.SelectAll();

            salesOrder.orderDate = DateTime.Today.Date;
            Session["order"] = salesOrder;
            return View(salesOrder);
        }
        public ActionResult Add(FormCollection formCollection)
        {
            foreach(string key in formCollection.AllKeys)
            {
                string k = key;
                string s = formCollection[key];
            }

            int productID = Convert.ToInt32(formCollection["products_select"]);
            int contactID = Convert.ToInt32(formCollection["contact.id"]);
            int quantity = Convert.ToInt32(formCollection["selectedProductQuantity"]);

            Product p = productGateway.Select(productID);
            SalesOrder order = Session["order"] as SalesOrder;
            if (order != null)
            {
                order.insertItem(p, quantity);
            }
            if (order.contact == null)
            {
                order.contact = "customer name";
            }
            Session["order"] = order;
            return View(order);
        }
        
        public ActionResult Checkout()
        {
            SalesOrder salesOrder = (SalesOrder)Session["order"];
            
            return View();
        }
    }
}