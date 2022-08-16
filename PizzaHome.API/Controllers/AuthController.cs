using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PizzaHome.Core.Entities;
using PizzaHome.Core.Interfaces;
using PizzaHome.ViewModels.Dtos;

namespace PizzaHome.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly IUserService _service;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;


        public AuthController(IUserService service, IAuthService authService, IMapper mapper)
        {
            _service = service;
            _authService = authService;
            _mapper = mapper;
        }


        [HttpPost("register")]
        public async Task<ActionResult> UserRegistration(User user)
        {

            await _service.AddUser(user);

            var accessToken = _authService.GenerateAccessToken(user.UserName);
            var refreshToken = _authService.GenerateRefreshToken(user.UserName);

            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }


        [HttpPost("login")]
        public async Task<ActionResult> UserAuthentication(UserDto user)
        {
            var mappedUser = _mapper.Map<User>(user);
            var result = await _service.CheckUser(mappedUser);

            if (result is null) return BadRequest();
           

            var accesstoken = _authService.GenerateAccessToken(user.UserName);
            var refreshtoken = _authService.GenerateRefreshToken(user.UserName);

            return Ok(new { AccessToken = accesstoken, RefreshToken = refreshtoken });

        }


        [HttpGet("refresh")]
        public ActionResult RefreshToken()
        {
            var refreshToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            var tokens = _authService.RegenerateTokens(refreshToken);
            if (tokens is null) return BadRequest("Invalid Token!");

            return Ok(tokens);

        }
    }
}
