using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // api/auth/login
        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            var userToLogin = _authService.Login(userLoginDto);
            if(!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            // create access token;
            var result = _authService.CreateAccessToken(userToLogin.Data);
            if(result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        // api/auth/register
        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto userRegisterDto)
        {
            // first check if the email already registered;
            var userExists = _authService.UserExists(userRegisterDto.Email);
            if(!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userRegisterDto);
            var result = _authService.CreateAccessToken(registerResult.Data);
            if(result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
