using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PizzaHome.API.ViewModels.Dtos;
using PizzaHome.Core.Entities;
using PizzaHome.Core.Interfaces;
using PizzaHome.ViewModels.Dtos;

namespace PizzaHome.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> UserRegistration(UserDto user)
        {
            var mappedUser = _mapper.Map<User>(user);
            await _service.AddUser(mappedUser);

            var accessToken = _authService.GenerateAccessToken(user.UserName, user.Role);
            var refreshToken = _authService.GenerateRefreshToken(user.UserName, user.Role);

            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> UserAuthentication(LoginDto user)
        {
            var mappedUser = _mapper.Map<User>(user);
            var result = await _service.GetUserByName(mappedUser.UserName);
            var mappedtoUserDto = _mapper.Map<UserDto>(result);

            if (result is null || !BCrypt.Net.BCrypt.Verify(mappedUser.Password, result.Password)) return BadRequest();
           

            var accesstoken  = _authService.GenerateAccessToken(user.UserName, mappedtoUserDto.Role);
            var refreshtoken = _authService.GenerateRefreshToken(user.UserName, mappedtoUserDto.Role);

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
