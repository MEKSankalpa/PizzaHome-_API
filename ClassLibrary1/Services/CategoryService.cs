using Dapper;
using PizzaHome.DataAccess;
using PizzaHome.Models;
using PizzaHome.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Services.Services
{
    public class CategoryService : ICategoryRepository
    {
        private readonly PizzaHomeDapperContext _context;

        public CategoryService(PizzaHomeDapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> Get()
        {
            string query = "SELECT * FROM Categories";
            using (var connection = _context.CreateConnection()) {
                var result = await connection.QueryAsync<Category>(query);
                return result.ToList();
            }
   
        }


        public async Task<Category> GetById(int id) {
            var query = "SELECT * FROM Categories WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Category>(query, new { id });
                return result;
            }
        }

      
        public async Task<Category> Add(Category category) {
            var query = "INSERT INTO Categories (Name, Description ) VALUES (@Name, @Description)" + "SELECT CAST(SCOPE_IDENTITY() as int)"; 

            var parameters = new DynamicParameters();
            parameters.Add("Name", category.Name, DbType.String);
            parameters.Add("Description", category.Description, DbType.String);

            using (var connection = _context.CreateConnection())
            {
               int id =  await connection.ExecuteAsync(query, parameters);
                var createdResult = new Category
                {
                    Id = id,
                    Name = category.Name,
                    Description = category.Description
                };
                return createdResult;


            }
        }
       
      public async Task UpdateCategory(int id, Category category) {

            var query = "UPDATE Categories SET Name = @Name, Description = @Description WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", category.Name, DbType.String);
            parameters.Add("Description", category.Description, DbType.String);
           
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }


        
             public async Task<Boolean> DeleteCategory(int id) {

                 var query1 = "SELECT * FROM Categories WHERE Id = @Id";
                 var query2 = "DELETE FROM Categories WHERE Id = @Id";

                 using (var connection = _context.CreateConnection())
                 {
                    var category = await connection.QuerySingleOrDefaultAsync<Category>(query1, new { id });

                      if (category is null)
                       {
                          return false;
                       }
                       else {

                           await connection.ExecuteAsync(query2, new { id });

                        }
                  }

                 return true;
        }

             
    }
}
