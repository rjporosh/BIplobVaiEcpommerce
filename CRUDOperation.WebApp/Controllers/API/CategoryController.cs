using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDOperation.WebApp.Controllers.API
{
    [EnableCors("AllowAll")]
    [Route("api/categories")]
    public class CategoryController : Controller
    {
        private readonly ICategoryManager _categoryManager;
        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var categories = _categoryManager.GetAll();
            return Ok(categories);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var category = _categoryManager.GetById(id);
            if (category == null)
            {
                return BadRequest("No Category Found");
            }
            return Ok(category);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Category model)
        {
            if (ModelState.IsValid)
            {
                var isSaved = _categoryManager.Add(model);
                if (isSaved)
                {
                    return Ok(model);
                }
            }

            return BadRequest(ModelState);

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}