﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Models;
using CRUDOperation.Models.RazorViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUDOperation.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private IOrderManager _orderManager;
        private IProductOrderManager _productOrderManager;
        private IProductManager _productManager;

        private IStockManager _stockManager;
        private IMapper _mapper;

        public OrderController(IOrderManager orderManager, IMapper mapper, IProductOrderManager productOrderManager, IStockManager stockManager, IProductManager productManager)
        {
            _orderManager = orderManager;
            _productOrderManager = productOrderManager;
            _stockManager = stockManager;
            _productManager = productManager;
            _mapper = mapper;
        }
        private void PopulateDropdownList(object selectList = null) /*Dropdown List Binding*/
        {
            // var category = _productManager.GetAll();
            //  ViewBag.SelectList= new SelectList(category, "Id", "Name", selectList);
            List<string> option = new List<string>();
            option.Add("Pending");
            option.Add("Rejected");
            option.Add("Cancelled");
            option.Add("Accepted");
            option.Add("Packed");
            option.Add("On The Way");
            option.Add("Delivered");
            ViewBag.SelectList = new SelectList(option, selectList);

        }
        private void PaymentMethodPopulateDropdownList(object selectList = null) /*Dropdown List Binding*/
        {
            // var category = _productManager.GetAll();
            //  ViewBag.SelectList= new SelectList(category, "Id", "Name", selectList);
            List<string> option = new List<string>();
            option.Add("Bkash");
            option.Add("Rocket");
            option.Add("Paypal");
            option.Add("Master/Visa Card");
            option.Add("On Delivery");
            ViewBag.PaymentMethodSelectList = new SelectList(option, selectList);

        }
        public IActionResult Index() //Search Facilities
        {


            var model = _orderManager.GetAll().ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            var cart = CRUDOperation.WebApp.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            var orders = _orderManager.GetAll();
            PopulateDropdownList();
            PaymentMethodPopulateDropdownList();
            var model = new OrderCreateViewModel();
            model.OrderDate = DateTime.Now;
            model.CustomerId = 10004;
            model.OrderNo = DateTime.Now.ToBinary().ToString() + model.CustomerId;
            model.ProductList = cart;
            model.OrderList = orders.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create([Bind("Id,CustomerId,OrderNo,OrderDate,Products,Customer,Status,ShippingAddress,PaymentMethod,ProductList,Phone")]OrderCreateViewModel model)
        {
            //var cart = Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            //if(model.ProductList == null)
            //{
            //    model.ProductList = cart;

            //}
            if (model.OrderNo == null || model.CustomerId == 0 || model.OrderDate == null)
            {
                model.OrderDate = DateTime.Now;
                model.CustomerId = 10004;
                model.OrderNo = DateTime.Now.ToString() + model.CustomerId;
            }
            // model.OrderNo = 
            if (ModelState.IsValid)
            {
                var order = _mapper.Map<Order>(model);
                if (_orderManager.OrderExists(model.Id))
                {
                    ViewBag.ErrorMessage = "Order Exists Already";
                }
                else
                {
                    order.Status = "Pending";

                    bool isAdded = _orderManager.Add(order);
                    long id = order.Id;

                    if (isAdded)
                    {
                        ProductOrder po = new ProductOrder();
                        Order o = _orderManager.GetById(id);
                        var cart = CRUDOperation.WebApp.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

                        foreach (var item in cart)
                        {
                            po.ProductId = item.Product.Id;
                            long pid = item.Product.Id;
                            po.Quantity = item.Quantity;
                            int qty = item.Quantity;
                            po.OrderId = id;
                            po.Customer = o.Customer;
                            po.CustomerId = o.CustomerId;
                            po.Status = "Pending";
                            _productOrderManager.Add(po);
                            //var stock = _stockManager.check(pid);
                            //stock.Quantity = stock.Quantity - qty;
                            //_stockManager.Update(stock);
                        }
                        cart.Clear();
                        CRUDOperation.WebApp.Helper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                        ViewBag.SuccessMessage = "Order Saved Successfully!";
                        //return nameof()
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Operation Failed!";
            }

            model.OrderList = _orderManager.GetAll().ToList();
            return View(model);
        }


        public PartialViewResult OrderListPartial()
        {
            var orders = _orderManager.GetAll();
            return PartialView("Order/_OrderList", orders);
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var order = _orderManager.GetById((Int64)Id);
            PopulateDropdownList();
            PaymentMethodPopulateDropdownList();
            OrderCreateViewModel aOrder = _mapper.Map<OrderCreateViewModel>(order);
            if (order == null)
            {
                return NotFound();
            }

            aOrder.OrderList = _orderManager.GetAll().ToList();
            return View(aOrder);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, [Bind("Id,CustomerId,OrderNo,OrderDate,Products,Customer,Status,ShippingAddress,PaymentMethod,Phone,Products")]OrderCreateViewModel order)
        {
            if (Id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var aOrder = _mapper.Map<Order>(order);
                bool isUpdated = _orderManager.Update(aOrder);
                if (isUpdated)
                {
                    var orders = _orderManager.GetAll();
                    ViewBag.SuccessMessage = "Order Updated Successfully!";
                    return View("Index", orders);

                }
                //}
            }
            else
            {
                ViewBag.ErrorMessage = "Update Failed!";
            }
            order.OrderList = _orderManager.GetAll().ToList();
            return View(order);

        }


        public IActionResult Delete(long id)
        {
            var customer = _orderManager.GetById(id);
            if (ModelState.IsValid)
            {
                bool isDeleted = _orderManager.Delete(customer);
                if (isDeleted)
                {
                    var orders = _orderManager.GetAll();
                    ViewBag.SuccessMessage = "Order Deleted Successfully.!";
                    return View("Index", orders);
                }

            }
            return RedirectToAction(nameof(Index));
        }
    }
}