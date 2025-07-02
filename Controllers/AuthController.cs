using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register(UserDto request)
        {
            bool registered = _userService.RegisterUser(request.Username, request.Password);
            if (!registered)
                return BadRequest("User already exists.");

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login(UserDto request)
        {
            var user = _userService.Authenticate(request.Username, request.Password);
            if (user == null)
                return Unauthorized("Invalid credentials.");

            // ✅ Create JWT token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                message = "Login successful.",
                token = tokenString
            });
        }
    }
}

