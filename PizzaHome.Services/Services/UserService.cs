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

        public async Task<User> GetUserByName(string email)
        {

            var query = "SELECT * FROM public.users WHERE email = @email";
            var user = await _service.Get<User>(query, new { email });

            return user;
        }

        public async Task<User> AddUser(User user) {

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var query = "INSERT INTO public.users (username, email, password, role ) VALUES (@UserName, @Email, @Password, @Role) returning id";
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
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
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
