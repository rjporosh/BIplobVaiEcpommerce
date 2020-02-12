using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDOperation.Abstractions.BLL;
using CRUDOperation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDOperation.WebApp.Controllers.API
{
    [Route("api/variants")]
    
    public class VariantController : ControllerBase
    {
        private readonly IVariantManager _variantManager;
        public VariantController(IVariantManager variantManager)
        {
            _variantManager = variantManager;
        }
        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var variants = _variantManager.GetAll();
            return Ok(variants);
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var variant = _variantManager.GetById(id);
            if (variant == null)
            {
                return BadRequest("No Variant Found");
            }
            return Ok(variant);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Variant model)
        {
            if (ModelState.IsValid)
            {
                var isSaved = _variantManager.Add(model);
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