using Dapper;
using PizzaHome.Core.Entities;
using PizzaHome.Core.Interfaces;
using PizzaHome.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DbService _service;
        

        public CategoryService(DbService service)
        {
            _service = service;
        }

        
        public async Task<IEnumerable<Category>> Get()
        {
            string query = "SELECT * FROM public.categories";
            var result = await _service.GetAll<Category>(query, new { });
            return result;

        }


        public async Task<Category> GetById(int id) {

            var query = "SELECT * FROM public.categories WHERE id = @id";
            var result = await _service.Get<Category>(query, new { id });
            return result;

        }

      
        public async Task<Category> Add(Category category) {
            var query = "INSERT INTO public.categories (name, description) VALUES (@Name, @Description)";

            int id = await _service.CreateAndEdit(query, category);
            var createdResult = new Category
            {
                Id = id,
                Name = category.Name,
                Description = category.Description
            };
            return createdResult;

        }
       
      public async Task UpdateCategory(int id, Category category) {

            var query = "UPDATE public.categories SET name = @Name, description = @Description WHERE id = @id";
            await _service.CreateAndEdit(query, category);
     
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var query = "DELETE FROM public.categories WHERE id = @id";
            var Id = await _service.CreateAndEdit(query, new { id });

            if (Id == 0)
            {
                return false;
            }
            return true;

        }




    }
}
