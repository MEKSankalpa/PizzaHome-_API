using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaHome.Models;
using PizzaHome.Services.Interfaces;

namespace PizzaHome.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

         
        
        //get all records 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories() {

                return Ok(await _service.Get());
        }

        [HttpGet("id", Name = "GetCategoryById")]
        public async Task<ActionResult<Category>> GetOneCategory(int id)
        {

            return Ok(await _service.GetById(id));
        }

        //Create category
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category) {

            var createdCategory =  await _service.Add(category);
            return CreatedAtRoute("GetCategoryById" , new { id = createdCategory.Id },createdCategory );
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
