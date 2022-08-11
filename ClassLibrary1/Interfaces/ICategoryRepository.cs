﻿using PizzaHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Services.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> Get();

        public  Task<Category> GetById(int id);


        public Task<Category> Add(Category category);
        
        public Task UpdateCategory(int id, Category category);
      
        public Task<bool> DeleteCategory(int id);

        
    }
}
