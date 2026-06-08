using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeDevOps.Api.Dtos;
using SeDevOps.Data;
using SeDevOps.Data.Entities;

namespace SeDevOps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public RolesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/roles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
    {
        var roles = await _context.Roles.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<RoleDto>>(roles));
    }

    // GET: api/roles/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<RoleDto>> GetRole(int id)
    {
        var role = await _context.Roles.FindAsync(id);

        if (role == null)
            return NotFound();

        return Ok(_mapper.Map<RoleDto>(role));
    }

    // POST: api/roles
    [HttpPost]
    public async Task<ActionResult<RoleDto>> CreateRole(RoleDto dto)
    {
        var role = _mapper.Map<Role>(dto);

        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<RoleDto>(role);

        return CreatedAtAction(nameof(GetRole), new { id = role.RoleId }, result);
    }

    // PUT: api/roles/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(int id, RoleDto dto)
    {
        if (id != dto.RoleId)
            return BadRequest("ID mismatch");

        var existing = await _context.Roles.FindAsync(id);
        if (existing == null)
            return NotFound();

        _mapper.Map(dto, existing);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/roles/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null)
            return NotFound();

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
