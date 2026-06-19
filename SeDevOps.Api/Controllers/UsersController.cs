using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeDevOps.Data;
using SeDevOps.Api.Dtos;
using SeDevOps.Data.Entities;
using AutoMapper;

namespace SeDevOps.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _context.Users.ToListAsync();

            var dtos = users.Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                DisplayName = u.DisplayName,
                Email = u.Email,
                RoleName = u.Role,
                CreatedAt = u.CreatedAt
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            var dto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                Email = user.Email,
                RoleName = user.Role,
                CreatedAt = user.CreatedAt
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(UserDto dto)
        {
            var user = new User
            {
                UserName = dto.UserName,
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                Role = dto.RoleName,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            dto.Id = user.Id;
            dto.CreatedAt = user.CreatedAt;

            return CreatedAtAction(nameof(Get), new { id = user.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.UserName = dto.UserName;
            user.DisplayName = dto.DisplayName;
            user.Email = dto.Email;
            user.Role = dto.RoleName;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
