using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDOperation.DatabaseContext;
using CRUDOperation.Models;
using CRUDOperation.Abstractions.BLL;
using AutoMapper;
using CRUDOperation.Models.RazorViewModels.Category;

namespace CRUDOperation.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        

        private readonly ICategoryManager _categoryManager;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryManager categoryManager, IMapper mapper)
        {
            _categoryManager = categoryManager;
            _mapper = mapper;
        }

        // GET: Category
        public IActionResult Index()
        {
            
            var model = _categoryManager.GetAll();

            return View(model);
        }

        // GET: Stock/Details/5
        public IActionResult Create()
        {
            var model = new CategoryCreateViewModel();
        
            return View(model);
        }

        [HttpPost]

        public IActionResult Create(CategoryCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(model); //AutoMapper
                
                //if(model.ParentId == null)
                //{
                //    model.ParentId = 0;
                //}
                bool isAdded = _categoryManager.Add(category);
                if (isAdded)
                {
                    ViewBag.SuccessMessage = "Saved Successfully!";
                }


            }
            else
            {
                ViewBag.ErrorMessage = "Operation Failed!";
            }
            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _categoryManager.GetById((Int64)id);
            
            CategoryCreateViewModel categoryCreateViewModel = _mapper.Map<CategoryCreateViewModel>(category);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Quantity,Unit,ProductId,ProductName")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool isUpdated = _categoryManager.Update(category);
                if (isUpdated)
                {
                    var categories = _categoryManager.GetAll();
                    ViewBag.SuccessMessage = "Stock Updated Successfully!";
                    return View("Index", categories);
                }
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _categoryManager.GetById(id);
            if (ModelState.IsValid)
            {
                bool isDeleted = _categoryManager.Delete(category);
                if (isDeleted)
                {
                    var categories = _categoryManager.GetAll();
                    ViewBag.SuccessMessage = "Data Deleted Successfully.!";
                    return View("Index", categories);
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
