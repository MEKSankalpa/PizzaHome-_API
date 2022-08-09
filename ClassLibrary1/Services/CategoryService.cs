using Microsoft.EntityFrameworkCore;
using PizzaHome.DataAccess;
using PizzaHome.Models;
using PizzaHome.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Services.Services
{
    public class CategoryService : ICategoryRepository
    {
        private readonly PizzaContext _context = new PizzaContext();
        public async Task<List<Category>> Get()
        {

            var categories = await _context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category> Add(Category category) {

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;    
        }

        public async Task UpdateCategory(int id, Category category) {

            _context.Entry(category).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e)
            {
             
                throw e;
            }
        }

        public async Task<Boolean> DeleteCategory(int id) {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
