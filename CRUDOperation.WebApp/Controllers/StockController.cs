using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUDOperation.Models;
using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Repositories;
using AutoMapper;
using CRUDOperation.Models.RazorViewModels.Stock;
using CRUDOperation.Abstractions.Repositories;

namespace CRUDOperation.WebApp.Controllers
{
    public class StockController : Controller
    {
        private IStockManager _stockManager;
        private IProductManager _productManager;


        private IMapper _mapper;

        private CRUDOperation.DatabaseContext.CRUDOperationDbContext _db; //Search Facilities

        public StockController(IStockManager stockManager, IMapper mapper, IProductManager productManager)
        {
            _mapper = mapper;
            _stockManager = stockManager;

            _productManager = productManager; //Dropdown List

            //_db = new CRUDOperation.DatabaseContext.CRUDOperationDbContext(); //Search Facilities
        }

        // GET: Stock
        public IActionResult Index()
        {
            var stocks = _stockManager.GetAll();
            var model = new StockCreateViewModel();
            model.StockList = stocks.ToList();
            PopulateDropdownList(); /*Dropdown List Binding*/
            return View(stocks);
        }

        // GET: Stock/Details/5
        public IActionResult Create()
        {
            var stocks = _stockManager.GetAll();
            var model = new StockCreateViewModel();
            model.StockList = stocks.ToList();
            PopulateDropdownList(); /*Dropdown List Binding*/
            return View(model);
        }

        [HttpPost]

        public IActionResult Create(StockCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var stock = _mapper.Map<Stock>(model); //AutoMapper

                bool isAdded = _stockManager.Add(stock);
                if (isAdded)
                {
                    ViewBag.SuccessMessage = "Saved Successfully!";
                }


            }
            else
            {
                ViewBag.ErrorMessage = "Operation Failed!";
            }

            model.StockList = _stockManager.GetAll().ToList();
            PopulateDropdownList(model.ProductId); /*Dropdown List Binding*/
            return View(model);
        }


        private void PopulateDropdownList(object selectList = null) /*Dropdown List Binding*/
        {
            var product = _productManager.GetAll();
            ViewBag.SelectList = new SelectList(product, "Id", "Name", selectList);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = _stockManager.GetById((Int64)id);
            PopulateDropdownList(stock.ProductId);
            StockCreateViewModel stockCreateViewModel = _mapper.Map<StockCreateViewModel>(stock);
            if (stock == null)
            {
                return NotFound();
            }
            stockCreateViewModel.StockList = _stockManager.GetAll().ToList();
            return View(stock);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Quantity,Unit,ProductId,ProductName")] Stock stock)
        {
            if (id != stock.Id)
            {
                return NotFound();
            }

            PopulateDropdownList(stock.ProductId);

            if (ModelState.IsValid)
            {
                bool isUpdated = _stockManager.Update(stock);
                if (isUpdated)
                {
                    var stocks = _stockManager.GetAll();
                    ViewBag.SuccessMessage = "Stock Updated Successfully!";
                    return View("Index", stocks);
                }
            }
            return View(stock);
        }

        public IActionResult Delete(int id)
        {
            var stock = _stockManager.GetById(id);
            if (ModelState.IsValid)
            {
                bool isDeleted = _stockManager.Delete(stock);
                if (isDeleted)
                {
                    var stocks = _stockManager.GetAll();
                    ViewBag.SuccessMessage = "Data Deleted Successfully.!";
                    return View("Index", stocks);
                }

            }
            return RedirectToAction(nameof(Index));
        }

        ////private bool ProductExists(int id)
        ////{
        ////    return _productManager.ProductExists(id);
        ////}

        //public IActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = _productManager.GetById((Int64)id);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}
    }
}
