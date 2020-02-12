using System;
using System.Linq;
using AutoMapper;
using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Models;
using CRUDOperation.Models.RazorViewModels.Customer;
using Microsoft.AspNetCore.Mvc;

namespace CRUDOperation.WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerManager _customerManager;
        private CRUDOperation.DatabaseContext.CRUDOperationDbContext _db; //Search Facilities

        private IMapper _mapper;

        public CustomerController(ICustomerManager customerManager,IMapper mapper)
        {
            _customerManager = customerManager;
            //_db = new CRUDOperation.DatabaseContext.CRUDOperationDbContext();

            _mapper = mapper;
        }
        //public IActionResult Index()
        //{
        //    var customers = _customerRepository.GetAll();
        //    //return View(customers);
        //    return RedirectToAction(nameof(Create));
        //}


        public IActionResult Index(string searchBy, string search) //Search Facilities
        {
            if (searchBy == "Address")
            {
                return View(_db.Customers.Where(x => x.Address.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(_db.Customers.Where(x => x.Name.StartsWith(search) || search == null).ToList());
            }
        }



        public IActionResult Create()
        {
            var customers = _customerManager.GetAll();
            var model = new CustomerCreateViewModel();
            model.CustomerList = customers.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CustomerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<Customer>(model); //AutoMapper

                if(_customerManager.CustomerExists(model.Name))
                {
                    ViewBag.ErrorMessage = "Customer already Exist";
                }
                else
                {
                    bool isAdded = _customerManager.Add(customer);
                    if (isAdded)
                    {
                        ViewBag.SuccessMessage = "Saved Successfully!";
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Operation Failed!";
            }

            model.CustomerList = _customerManager.GetAll().ToList();
            return View(model);
        }

        //public PartialViewResult CustomerListPartial()
        //{
        //    var customers = _customerRepository.GetAll();
        //    return PartialView("Customer/_CustomerList", customers);
        //}


        public IActionResult Edit(int? id)
        {

            if (id ==null)
            {
                return NotFound();
            }

            var customer = _customerManager.GetById((Int64)id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Address,LoyaltyPoint")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool isUpdated = _customerManager.Update(customer);
                if (isUpdated)
                {
                    var customers = _customerManager.GetAll();
                    ViewBag.SuccessMessage = "Updated Successfully!";
                    return View("Index", customers);
                    
                }
            }
            return View(customer);
            
        }


       
        public IActionResult Delete(int id)
        {
            var customer = _customerManager.GetById(id);
            if (ModelState.IsValid)
            {
                bool isDeleted = _customerManager.Delete(customer);
                if (isDeleted)
                {
                    var customers = _customerManager.GetAll();
                    ViewBag.SuccessMessage = "Data Deleted Successfully.!";
                    return View("Index", customers);
                }

            }
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(string name)
        {
            return _customerManager.CustomerExists(name);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _customerManager.GetById((Int64)id);

            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
    }
}