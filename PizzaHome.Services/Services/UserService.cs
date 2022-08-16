using PizzaHome.Core.Entities;
using PizzaHome.Core.Interfaces;
using PizzaHome.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Services.Services
{
    public class UserService : IUserService
    {

        private readonly DbService _service;

        public UserService(DbService service)
        {
            _service = service;
        }


        public async Task<List<User>> GetAllUsers() {

            var query = "SELECT * FROM public.users";
            var result = await _service.GetAll<User>(query, new { });

            return result;

        }

        public async Task<User> GetUserById(int id) {

            var query = "SELECT * FROM public.users WHERE id=@id";
            var result = await _service.Get<User>(query, new { id});

            return result;
        }

        public async Task<User> GetUserByName(string name)
        {

            var query = "SELECT * FROM public.users WHERE username = @UserName";
            var user = await _service.Get<User>(query, new { name });

            return user;
        }


        //This will call when it is user trying to login using his username and password
        public async Task<User> CheckUser(User user)
        {

            var query = "SELECT * FROM public.users WHERE username = @UserName AND password = @Password";
            var result = await _service.Get<User>(query, user);

            return result;
        }


        public async Task<User> AddUser(User user) {

            var query = "INSERT INTO public.users (username, email, password, role ) VALUES (@UserName, @Email, @Password, @Role) RETURNING id";
            var id = await _service.CreateAndEdit(query, user);

            return new User {
              Id = id,
              UserName = user.UserName,
              Email = user.Email,
              Password = user.Password,
              Role = user.Role
            };

        }

        public async Task UpdateUser(int id,User user)
        {

            var query = "UPDATE public.users SET username = @UserName, email = @Email, password = @Password, role = @Role WHERE id = @id";
            await _service.CreateAndEdit(query, user);

        }


        public async Task<bool> DeleteUser(int id) {

            var query = "DELETE FROM public.users WHERE id = @id";
            var Id = await _service.CreateAndEdit(query, new {id});

            if (Id == 0) return false;

            return true;
        }


    }
}
