﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaHome.Models;
using PizzaHome.Services.Interfaces;

namespace PizzaHome.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {


        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        
        }

        
        // GET: api/Products
        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<Product>>> Index()
        {
            var result = await _service.GetProducts();
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<ActionResult<Product>> IndexById(int id)
        {
            var result = await _service.GetProductById(id);
            return Ok(result);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await _service.UpdateProduct(id, product);
            return NoContent();
          
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            var createdProduct =await _service.CreateProduct(product);
            return CreatedAtRoute("GetCategoryById", new { id = createdProduct.Id }, createdProduct);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _service.DeleteProduct(id);
            if (result == false)
            {
                return NotFound();
            }

            return NoContent();
        }

      /*  [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> DeleteMultipleProduct([FromQuery]int[] ids)
        {
            var products = new List<Product>();

            foreach (var id in ids) {
                var result = await _context.Product.FindAsync(id);
                if (result == null) {
                    return NotFound();
                }

                products.Add(result);
            }

            _context.Product.RemoveRange(products);
            await _context.SaveChangesAsync();

            return Ok(products);

        }

     */

       
    }


}
