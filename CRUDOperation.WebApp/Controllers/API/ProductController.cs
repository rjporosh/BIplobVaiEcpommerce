using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Models;
using CRUDOperation.Models.APIViewModels;
using CRUDOperation.Models.RazorViewModels.Product;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CRUDOperation.WebApp.Controllers.API
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("api/products")]
    //[Route("api/products.{format}"), FormatFilter]
    public class ProductController : ControllerBase
    {
        private IProductManager _productManager;

        private IMapper _mapper;

        public ProductController(IProductManager productManager, IMapper mapper)
        {
            _productManager = productManager;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] ProductSearchCriteriaVM criteria) //Just serach facilities GetByCriteria method
        {
            var products = _productManager.GetByCriteria(criteria);


            if (products != null && products.Any())
            {
                var productCreateViewModels = _mapper.Map<ICollection<ProductDto>>(products);
                return Ok(productCreateViewModels);
            }

            return NoContent();
            //.Select(p => new //if i want to show data like this format in browser then need this select option
            // {
            //     p.Id,
            //     p.Name,
            //     Category = new
            //     {
            //         CategoryId = p.Category.Id,
            //         CategoryName = p.Category.Name
            //     },
            //     p.IsActive,
            //     p.ExpireDate,
            //     p.Price,
            //     p.ImageUrl
            // });

            //if (products != null && products.Any())
            //{
            //    return Ok(products);
            //}
            //return NoContent();
        }

        [HttpGet("{id}")]
        //[AcceptVerbs, HttpGet]
        public IActionResult GetProductById(long id)
        {
            var product = _productManager.GetById(id);

            if(product==null)
            {
                return BadRequest("Product Not Found.!!");
            }
            return Ok(product);
        }
        
        public IActionResult Post(Product product)
        {
            if(ModelState.IsValid)
            {
                var isAdded = _productManager.Add(product);
                if(isAdded)
                {
                    return Created("/api/products/"+product.Id, product);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] Product model)
        {
            var product = _productManager.GetById(id);
            if (product == null)
            {
                return BadRequest("No Product Found to Update!");
            }
            if (ModelState.IsValid)
            {

                product.Name = model.Name;
                product.Price = model.Price;
                product.CategoryId = model.CategoryId;
                product.IsActive = model.IsActive;
                product.ExpireDate = model.ExpireDate;
                //product.ImageUrl = model.ImageUrl;

                bool isUpdated = _productManager.Update(product);
                if (isUpdated)
                {
                    return Ok();
                }
            }
            return BadRequest(ModelState);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAPI(long id)
        {
            var product = _productManager.GetById(id);
            if (product == null)
            {
                return BadRequest("No Product found to Delete");
            }

            var isRemoved = _productManager.Delete(product);
            if (isRemoved)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}