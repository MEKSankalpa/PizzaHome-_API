using PizzaHome.Models;
using PizzaHome.DataAccess;
using PizzaHome.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Data;

namespace PizzaHome.Services.Services
{
    public class ShopService : IShopRepository
    {

        private readonly PizzaHomeDapperContext _context;

        public ShopService(PizzaHomeDapperContext context)
        {
            _context = context;
        }

        public  async Task<List<Shop>> GetAllShops()
        {
            string query = "SELECT * FROM Shops";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Shop>(query);
                return result.ToList();
            }
        }

        public async Task<Shop> GetShopById(int id)
        {
            var query = "SELECT * FROM Shops WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Shop>(query, new { id });
                return result;
            }
        }

        public async Task<Shop> CreateShop(Shop shop) {

            var query = "INSERT INTO Shops (ShopName, ShopLocation, Status ) VALUES (@ShopName, @ShopLocation, @Status)" + "SELECT CAST(SCOPE_IDENTITY() as int)"; 

            var parameters = new DynamicParameters();
            parameters.Add("ShopName", shop.ShopName, DbType.String);
            parameters.Add("ShopLocation", shop.ShopLocation, DbType.String);
            parameters.Add("Status", shop.Status, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                int id = await connection.ExecuteAsync(query, parameters);
                var createdResult = new Shop
                {
                    Id = id,
                    ShopName = shop.ShopName,
                    ShopLocation = shop.ShopLocation,
                    Status = shop.Status
                };

                return createdResult;


            }
        }

        public async Task UpdateShop(int id, Shop shop) {
            var query = "UPDATE Shops SET ShopName = @Name, ShopLocation = @Location, Status = @Status WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", shop.ShopName, DbType.String);
            parameters.Add("Location", shop.ShopLocation, DbType.String);
            parameters.Add("Status", shop.Status, DbType.Int32);


            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }

        public async Task<Boolean> DeleteShop(int id) {
            var query1 = "SELECT * FROM Shops WHERE Id = @Id";
            var query2 = "DELETE FROM Shops WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var category = await connection.QuerySingleOrDefaultAsync<Shop>(query1, new { id });

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
