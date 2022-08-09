using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaHome.Models;
using PizzaHome.Services.Interfaces;

namespace PizzaHome.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _service;

        public CategoryController(ICategoryRepository repository)
        {
            _service = repository;
        }


        
        //get all records 
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories() {

                return Ok(await _service.Get());
        }

       
        //Create category
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category) {

            await _service.Add(category);
            return NoContent();
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, Category category) {
            if (id != category.Id) {
                return BadRequest();
            }

            await _service.UpdateCategory(id, category);
            return NoContent();

        }

        

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id) {

            var res = await  _service.DeleteCategory(id);
            if (res == false)
            {
                return NotFound();
            }
            return NoContent();

        } 
    }
}
