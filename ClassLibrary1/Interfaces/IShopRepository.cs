using PizzaHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Services.Interfaces
{
    public interface IShopRepository
    {
        public Task<List<Shop>> GetAllShops();

        public  Task<Shop> GetShopById(int id);
        public Task<Shop> CreateShop(Shop shop);

        public Task UpdateShop(int id, Shop shop);

        public  Task<bool> DeleteShop(int id);
    }
}
