using Dapper;
using Microsoft.EntityFrameworkCore;
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
    public class ProductService : IProductRepository
    {
        private readonly PizzaHomeDapperContext _context;

        public ProductService(PizzaHomeDapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts() {
            string query = "SELECT * FROM Product";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Product>(query);
                return result.ToList();
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            var query = "SELECT * FROM Product WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Product>(query, new { id });
                return result;
            }
        }

        public async Task<Product> CreateProduct(Product product) {
            var query = "INSERT INTO Product (Name, Description, Price, CategoryId ) VALUES (@Name, @Description, @Price, @CategoryId)" + "SELECT CAST(SCOPE_IDENTITY() as int)"; 

            var parameters = new DynamicParameters();
            parameters.Add("Name", product.Name, DbType.String);
            parameters.Add("Description", product.Description, DbType.String);
            parameters.Add("Price", product.Price, DbType.Int64);
            parameters.Add("CategoryId", product.CategoryId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                int id = await connection.ExecuteAsync(query, parameters);
                var createdResult = new Product
                {
                    Id = id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    CategoryId = product.CategoryId,    
                };
                return createdResult;


            }
        }

        public async Task UpdateProduct(int id, Product product) {

            var query = "UPDATE Product SET Name = @Name, Description = @Description, Price = @Price, CategoryId =@CategoryId   WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Name", product.Name, DbType.String);
            parameters.Add("Description", product.Description, DbType.String);
            parameters.Add("Price", product.Price, DbType.Int64);
            parameters.Add("CategoryId", product.CategoryId, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<Boolean> DeleteProduct(int id) {
            var query1 = "SELECT * FROM Product WHERE Id = @Id";
            var query2 = "DELETE FROM Product WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var category = await connection.QuerySingleOrDefaultAsync<Product>(query1, new { id });

                if (category is null)
                {
                    return false;
                }
                else
                {
                    await connection.ExecuteAsync(query2, new { id });
                }
            }

            return true;
        }
    }
}
