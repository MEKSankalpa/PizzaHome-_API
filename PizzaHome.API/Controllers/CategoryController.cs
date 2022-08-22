using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaHome.Core.Dtos;
using PizzaHome.Core.Entities;
using PizzaHome.Core.Interfaces;

namespace PizzaHome.Controllers
{
    [Authorize]
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly IMapper _mapper;

       
        public CategoryController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //get all records 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories() {

            var categories = await _service.Get();
            var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(mappedCategories);

        }

        
        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<ActionResult<CategoryDto>> GetOneCategory(int id)
        {
            var category = await _service.GetById(id);
            if (category is null) throw new KeyNotFoundException("Category Not Found!");
            var mappedCategory = _mapper.Map<CategoryDto>(category);
            return Ok(mappedCategory);
        }

        //Create category
       
        [HttpPost]
        [Authorize(Policy = "PizzaHomeManagementPolicy")]
        public async Task<IActionResult> AddCategory(Category category) {

            var createdCategory =  await _service.Add(category);
            var mappedCategory = _mapper.Map<CategoryDto>(createdCategory);
            return CreatedAtRoute("GetCategoryById" , new { id = mappedCategory.Id }, mappedCategory);
        }

        [Authorize]
        [HttpPut("{id}")]
        [Authorize(Policy = "PizzaHomeManagementPolicy")]
        public async Task<ActionResult> UpdateCategory(int id, Category category) {

           if (id != category.Id)  throw new ApplicationException("Category Not Found!");
; 
           await _service.UpdateCategory(id, category);
           return NoContent();

       }


     
        [HttpDelete("{id}")]
        [Authorize(Policy = "PizzaHomeManagementPolicy")]
        public async Task<ActionResult> DeleteCategory(int id) {

          var res = await  _service.DeleteCategory(id);
          if (res == false) throw new ApplicationException("Category Not Found!"); 

          return NoContent();

      } 

      
    }
}
