using InventoryApp.Models;
using InventoryApp.Security.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace InventoryApp.Security
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly StockDbContext _context;
        private readonly IConfiguration _config;

        public AuthController(StockDbContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterUser registerUser)
        {
            if (_context.Users.Any(u => u.UserName == registerUser.Username))
                return BadRequest("Username already exists");

            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                UserName = registerUser.Username,
                Email = registerUser.Email,
                Role = registerUser.Role
            };

            user.PasswordHash = hasher.HashPassword(user, registerUser.Password);

            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login(Login login)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == login.Username);
            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid username or password");
            }

            var claims = new List<Claim>
            {
                new (ClaimTypes.Name, user.UserName),
                new (ClaimTypes.Role, user.Role)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

    }
}
