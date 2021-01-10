using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Models;
using Inventory.DAL;

namespace Inventory.Controllers
{
    public class EmployeeSalesController : Controller
    {
        CategoryGateway categoryGateway = new CategoryGateway();
        EmployeeProductGateway productGateway = new EmployeeProductGateway();
        ContactGateway contactGateway = new ContactGateway();
        EmployeeSalesGateway employeeSales = new EmployeeSalesGateway();

        // GET: EmployeeSales
        public ActionResult Index()
        {
            return View(employeeSales.SelectAll());
        }

        public ActionResult Create()
        {
            SalesOrder salesOrder = new SalesOrder();
            salesOrder.categoryList = new SelectList(categoryGateway.SelectAll(), "id", "name");
            salesOrder.productList = new SelectList(productGateway.SelectAll(), "id", "name");

            salesOrder.categories = categoryGateway.SelectAll();
            salesOrder.products = productGateway.SelectAll();

            salesOrder.odate = DateTime.Now.ToString();
            salesOrder.orderDate = DateTime.Now;
            Session["order"] = salesOrder;
            return View(salesOrder);
        }

        public ActionResult Add(FormCollection form)
        {
            foreach (string key in form.AllKeys)
            {
                string k = key;
                string s = form[key];
            }

            int productID = Convert.ToInt32(form["products_select"]);
            int quantity = Convert.ToInt32(form["selectedProductQuantity"]);

            SalesOrder order = Session["order"] as SalesOrder;

            

            foreach(Product pro in order.products)
            {
                if(pro.id == productID)
                {
                    if (quantity > pro.stock)
                    {
                        return View(order);
                    }
                    pro.stock -= quantity;
                }
            }

            if(form["contact"] != null)
            {
                order.contact = form["contact"];
            }
            if(order != null)
            {
                string odate = order.odate;
            }
            

            if(productID != 0)
            {
                Product p = productGateway.Select(productID);
                order.insertItem(p, quantity);
            }

            
            Session["order"] = order;
            return View(order);

        }

        public ActionResult Checkout()
        {
            SalesOrder order = Session["order"] as SalesOrder;
            employeeSales.Insert(order);
            return RedirectToAction("Index");
        }

    }
}