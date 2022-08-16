using PizzaHome.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Core.Interfaces
{
    public interface IShopService
    {
        public Task<List<Shop>> GetAllShops();

        public  Task<Shop> GetShopById(int id);
        public Task<Shop> CreateShop(Shop shop);

        public Task UpdateShop(int id, Shop shop);

        public  Task<bool> DeleteShop(int id);
    }
}
