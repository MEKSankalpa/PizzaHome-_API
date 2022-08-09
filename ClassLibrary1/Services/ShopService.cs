using PizzaHome.Models;
using PizzaHome.DataAccess;
using PizzaHome.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PizzaHome.Services.Services
{
    public class ShopService : IShopRepository
    {

        private readonly PizzaContext _context = new PizzaContext();

        public  async Task<List<Shop>> GetAllShops()
        {
            var result =await _context.Shops.ToListAsync();
            return result;
        }

        public async Task<Shop> CreateShop(Shop shop) { 

           await _context.Shops.AddAsync(shop);
           await _context.SaveChangesAsync();

           return shop;
        }

        public async Task<Shop> UpdateShop(int id, Shop shop) {
            _context.Entry(shop).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e)
            {

                throw e;
            }
            return shop;

        }

        public async Task<Boolean> DeleteShop(int id) {
            var shop = await _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return false;
            }
            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
