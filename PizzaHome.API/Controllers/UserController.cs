using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using PizzaHome.Core.Entities;
using PizzaHome.Core.Interfaces;
using PizzaHome.ViewModels.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PizzaHome.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
           
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> index() {

            var users = await _service.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<User>> indexById(int id) {

            var user = await _service.GetUserById(id);

            return Ok(user);    
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(UserDto user)
        {
            var mappedUser = _mapper.Map<User>(user);
            var id = await _service.AddUser(mappedUser);

            return CreatedAtRoute("GetUserById", new { Id = id}, user );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user) {

            if (id != user.Id) return BadRequest();
            await _service.UpdateUser(id,user);
         
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteUser(id);
            if (result == false) return BadRequest();

            return NoContent();
        }

    }
}
