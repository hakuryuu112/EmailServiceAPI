using Azure.Core;
using EmailServiceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace EmailServiceAPI.Controllers.Users
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DBContext.DatabaseContext _context;

        public UserController(DBContext.DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost("users")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequest user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return BadRequest(new { message = "User with this email already exists" });
            }

            var UserModel = new UserModel
            {
                Id = Guid.NewGuid(),
                RoleId = user.RoleId,
                Username = user.Username,
                Email = user.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash), // install BCrypt.Net-Next
                Department = user.Department,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Admin",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = "Admin"
            };

            _context.Users.Add(UserModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User created successfully", user });
        }

        [HttpGet("users")]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userRole))
            {
                return Unauthorized();
            }

            IQueryable<UserModel> usersQuery = _context.Users;

            if (userRole == "Manager")
            {
                var currentUser = await _context.Users.FindAsync(Guid.Parse(userId));
                if (currentUser != null)
                {
                    usersQuery = usersQuery.Where(u => u.Department == currentUser.Department);
                }
            }
            else if (userRole == "Employee")
            {
                usersQuery = usersQuery.Where(u => u.Id.ToString() == userId);
            }

            var users = await usersQuery.ToListAsync();

            return Ok(users);
        }
    }
}
