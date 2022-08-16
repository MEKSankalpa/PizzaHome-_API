using PizzaHome.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Core.Interfaces
{
    public interface ICategoryService
    {
        
        public Task<IEnumerable<Category>> Get();

        public  Task<Category> GetById(int id);

        public Task<Category> Add(Category category);
        
        public Task UpdateCategory(int id, Category category);
      
        public Task<bool> DeleteCategory(int id);  

        
    }
}
