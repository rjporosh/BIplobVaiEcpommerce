using System.Linq;
using CRUDOperation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Models.RazorViewModels.Product;
using System;
using AutoMapper;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CRUDOperation.Models.APIViewModels;
//using System.Data.Entity;
using ReflectionIT.Mvc.Paging;

namespace CRUDOperation.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManager _productManager;
        private readonly ICategoryManager _categoryManager;

        private readonly IMapper _mapper;


        private readonly CRUDOperation.DatabaseContext.CRUDOperationDbContext _db; //Search Facilities

        public ProductController(IProductManager productManager, ICategoryManager categoryManager, IMapper mapper)
        {
            _productManager = productManager;
            _categoryManager = categoryManager; //Dropdown List

            _mapper = mapper;

            _db = new CRUDOperation.DatabaseContext.CRUDOperationDbContext(); //Search Facilities
        }

        [AllowAnonymous]
        public IActionResult Index(string searchBy, string search,double price) //Search Facilities
        {
            //return View(_db.Products.Where(x => x.Name.StartsWith(search) || search == null).ToList());
            if (searchBy == "Price")
            {
                return View(_productManager.GetByPrice(price));
            }
            else if (searchBy == "Name")
            {
                return View(_productManager.GetByName(search));
            }
            else if (searchBy == "CategoryName")
            {
                return View(_productManager.GetByCategory(search));
            }


            var model = _productManager.GetAll();

            return View(model);
        }
        //[Authorize]
        [AllowAnonymous]
        public IActionResult ProductListIndex()
        {
            var products = _productManager.GetAll();
            return View(products);
        }

        //public async Task<IActionResult> ProductList(int page=1)
        //{
        //    var query = _db.Products.AsNoTracking().OrderBy(p => p.Name);
        //    var model = await PagingList.CreateAsync(query, 5, page);
        //    return View(model);
        //}


        public IActionResult Create()
        {
            var products = _productManager.GetAll();
            var model = new ProductCreateViewModel();
            model.ProductList = products.ToList();


            PopulateDropdownList(); /*Dropdown List Binding*/
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Create(ProductCreateViewModel model, IFormFile ImageUrl /*List<IFormFile> ImageUrl*/)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(model); //AutoMapper

                /*Save Images start*/


                using (var ms = new MemoryStream())
                {
                    ImageUrl.CopyTo(ms);
                    product.ImageUrl = ms.ToArray();
                    // ImageConverter converter = new ImageConverter();
                    // model.Image = (byte[])converter.ConvertTo(Image, typeof(byte[]));
                }
                //test
                var files = HttpContext.Request.Form.Files;
                foreach (var image in files)
                {
                    if (image != null && image.Length > 0)
                    {
                        var file = ImageUrl;
                        // var root = _appEnvironment.WebRootPath;
                        var root = "wwwroot\\";
                        var uploads = "uploads\\img";
                        if (file.Length > 0)
                        {
                            // you can change the Guid.NewGuid().ToString().Replace("-", "")
                            // to Guid.NewGuid().ToString("N") it will produce the same result
                            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);

                            using (var fileStream = new FileStream(Path.Combine(root, uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                // This will produce uploads\img\fileName.ext
                                product.ImagePath = Path.Combine(uploads, fileName);
                            }
                        }
                    }
                }
                bool isAdded = _productManager.Add(product);
                if (isAdded)
                {
                    ViewBag.SuccessMessage = "Saved Successfully!";
                }
                //foreach (var item in ImageUrl)
                //{
                //    if(item.Length>0)
                //    {
                //        using (var ms = new MemoryStream())
                //        {
                //            await item.CopyToAsync(ms);
                //            product.ImageUrl = ms.ToArray();

                //            bool isAdded = _productManager.Add(product);
                //            if (isAdded)
                //            {
                //                ViewBag.SuccessMessage = "Saved Successfully!";
                //            }
                //        }
                //    }

                //}
                /*Image save End*/
            }
            else
            {
                ViewBag.ErrorMessage = "Operation Failed!";
            }

            model.ProductList = _productManager.GetAll().ToList();
            PopulateDropdownList(model.CategoryId); /*Dropdown List Binding*/
            return View(model);
        }


        private void PopulateDropdownList(object selectList=null) /*Dropdown List Binding*/
        {
            var category = _categoryManager.GetAll();
            ViewBag.SelectList = new SelectList(category, "Id", "Name", selectList);
        }

        public IActionResult Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var product = _productManager.GetById((Int64)id);
            PopulateDropdownList(product.CategoryId);
            ProductCreateViewModel productCreateViewModel = _mapper.Map<ProductCreateViewModel>(product);
            //productCreateViewModel.Stock.Quantity = product.Stock.Quantity;
            if (product == null)
            {
                return NotFound();
            }



            //var productCreateViewModel = new ProductCreateViewModel();

            productCreateViewModel.ProductList = _productManager.GetAll().ToList();

            //productCreateViewModel.Name = product.Name;
            //productCreateViewModel.Price = product.Price;
            //productCreateViewModel.ExpireDate = product.ExpireDate;
            //productCreateViewModel.IsActive = product.IsActive;
            //productCreateViewModel.Category = product.Category;
            //productCreateViewModel.CategoryId = Convert.ToInt32(product.CategoryId);
            //productCreateViewModel.ImageUrl = product.ImageUrl;
            return View(productCreateViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ExpireDate,IsActive,CategoryId,CategoryName,ImagePath,ImageUrl")] ProductCreateViewModel product, IFormFile ImageUrl)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            if (ImageUrl != null)
            {
                using (var ms = new MemoryStream())
                {
                    ImageUrl.CopyTo(ms);
                    
                    product.ImageUrl = ms.ToArray();
                    var files = HttpContext.Request.Form.Files;
                    foreach (var imageUrl in files)
                    {
                        if (imageUrl != null && imageUrl.Length > 0)
                        {
                            var file = ImageUrl;
                            var root = "wwwroot\\";
                            var uploads = "uploads\\img";
                            if (file.Length > 0)
                            {
                                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);

                                using (var fileStream = new FileStream(Path.Combine(root, uploads, fileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                    product.ImagePath = Path.Combine(uploads, fileName);
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                product.ImageUrl = product.ImageUrl;
                product.ImagePath = product.ImagePath;
            }

            PopulateDropdownList(product.CategoryId);

            if (ModelState.IsValid)
            {
                var productCreateViewModel = _mapper.Map<Product>(product);

                productCreateViewModel.ImageUrl = product.ImageUrl;
                productCreateViewModel.ImagePath = product.ImagePath;

                bool isUpdated = _productManager.Update(productCreateViewModel);
                if (isUpdated)
                {
                    var products = _productManager.GetAll();
                    ViewBag.SuccessMessage = "Updated Successfully!";
                    return View("Index", products);
                }
            }
            return View(product);
        }


        public IActionResult Delete(long id)
        {
            var product = _productManager.GetById(id);
            if (ModelState.IsValid)
            {
                bool isDeleted = _productManager.Delete(product);
                if (isDeleted)
                {
                    var products = _productManager.GetAll();
                    ViewBag.SuccessMessage = "Data Deleted Successfully.!";
                    return View("Index", products);  
                }

            }
            return RedirectToAction(nameof(Index));
        }

        //private bool ProductExists(int id)
        //{
        //    return _productManager.ProductExists(id);
        //}

        public IActionResult Details(long id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var product = _productManager.GetById(id);

            //if (product == null)
            //{
            //    return NotFound();
            //}
            //return View(product);

            //var product = _productManager.GetAll();
            return View(product);

        }


        public IActionResult Show()
        {
            return View();
        }
        public IActionResult GetProductPartial(long id)
        {
            var product = _productManager.GetById(id);
            if (product == null)
            {
                return null;
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return PartialView("Product/_ProductDetails", productDto);

        }


        public IActionResult Variants()
        {
            return View();
        }

        #region
        //public List<Product> CategoryWiseProductLoad(int? category)
        //{
        //    if(category !=null)
        //    {
        //        var products = _db.Products
        //                          .OrderByDescending(x => x.Id)
        //                          .Where(x => x.CategoryId == category) 
        //                          .ToList();

        //        return products;
        //    }
        //    else
        //    {
        //        var products = _db.Products.OrderByDescending(x => x.Id).ToList();
        //        return products;
        //    }

        //}
        #endregion
    }
}


