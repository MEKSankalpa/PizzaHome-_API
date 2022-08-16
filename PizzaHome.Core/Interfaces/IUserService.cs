using PizzaHome.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Core.Interfaces
{
    public interface IUserService
    {
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserById(int id);
        public Task<User> GetUserByName(string name);
        public Task<User> AddUser(User user);
        public  Task UpdateUser(int id, User user);
        public Task<User> CheckUser(User user);
        public Task<bool> DeleteUser(int id);


    }
}
