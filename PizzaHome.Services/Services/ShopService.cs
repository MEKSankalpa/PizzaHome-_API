using PizzaHome.Models;
using PizzaHome.DataAccess;
using PizzaHome.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Data;

namespace PizzaHome.Services.Services
{
    public class ShopService : IShopService
    {

        private readonly DbService _service;

        public ShopService(DbService service)
        {
            _service = service;
        }

        public  async Task<List<Shop>> GetAllShops()
        {
            string query = "SELECT * FROM public.shops";
            var shops = await _service.GetAll<Shop>(query, new { });
            return shops;
        }

        public async Task<Shop> GetShopById(int id)
        {
            var query = "SELECT * FROM public.shops WHERE id = @id";
            var shop = await _service.Get<Shop>(query, new { id }); 
            return shop;    
        }

        public async Task<Shop> CreateShop(Shop shop) {

            var query = "INSERT INTO public.shops (shopname, addressno, street, city, status ) VALUES (@ShopName, @AddressNo, @Street, @City, @Status) RETURNING id";
            int id = await _service.CreateAndEdit(query, shop);
            return new Shop
            {
                Id = id,
                ShopName = shop.ShopName,
                AddressNo = shop.AddressNo,
                Street = shop.Street,
                City = shop.City,
                Status = shop.Status
            };
          
        }

        public async Task UpdateShop(int id, Shop shop) {

            var query = "UPDATE public.shops SET shopname = @ShopName, addressno = @AddressNo, street = @Street, city = @City, status = @Status WHERE id = @id";
            await _service.CreateAndEdit(query, shop);

        }

        public async Task<bool> DeleteShop(int id) {
         
            var query = "DELETE FROM public.shops WHERE id = @id";
            int Id = await _service.CreateAndEdit(query, new {id});
            if (Id == 0) { 
                return false;
            }
            return true;
        }


    }
}
