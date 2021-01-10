using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inventory.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel;

namespace Inventory.Models
{
    public class SalesOrder
    {
        public int id { get; set; }
        [DisplayName("Customer Name")]
        public string contact { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Order Date")]
        public DateTime orderDate { get; set; }
        public string odate { get; set; }
        public List<CartItem> cart { get; set; }
        public double total { get; set; }
        public string productName { get; set; }
        public string branch { get; set; }

        public SelectList contactList { get; set; }
        public SelectList categoryList { get; set; }
        public SelectList productList { get; set; }

        public List<Contact> contacts { get; set; }
        public List<Category> categories { get; set; }
        public List<Product> products { get; set; }

        
        public int selectedProductId { get; set; }
        [DisplayName("Quantity")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number")]
        public int selectedProductQuantity { get; set; }

        public void insertItem(Product product, int quantity)
        {
            CartItem newCartItem = new CartItem();
            newCartItem.Product = product;
            newCartItem.Quantity = quantity;
            newCartItem.CalculateSubtotal();
            if(cart==null)
            {
                cart = new List<CartItem>();
            }
            foreach(CartItem item in cart)
            {
                if(item.Product.id == newCartItem.Product.id)
                {
                    item.Quantity += quantity;
                    item.CalculateSubtotal();
                    return;
                }
            }
            cart.Add(newCartItem);
        }

        public void removeItem(int productId)
        {
            foreach(CartItem cartItem in cart)
            {
                if(cartItem.Product.id == productId)
                {
                    cart.Remove(cartItem);
                }
            }
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (CartItem item in cart)
            {
                total += item.CalculateSubtotal();
            }
            return total;
        }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }
        public double CalculateSubtotal()
        {
            this.SubTotal = this.Product.retailPrice * this.Quantity;
            return this.SubTotal;
        }
    }
}