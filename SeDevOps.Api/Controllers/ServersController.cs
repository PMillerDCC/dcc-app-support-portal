using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeDevOps.Api.Dtos;
using SeDevOps.Data;
using SeDevOps.Data.Entities;

namespace SeDevOps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServersController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ServersController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/servers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServerDto>>> GetServers()
    {
        var servers = await _context.Servers.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<ServerDto>>(servers));
    }

    // GET: api/servers/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ServerDto>> GetServer(int id)
    {
        var server = await _context.Servers.FindAsync(id);

        if (server == null)
            return NotFound();

        return Ok(_mapper.Map<ServerDto>(server));
    }

    // POST: api/servers
    [HttpPost]
    public async Task<ActionResult<ServerDto>> CreateServer(ServerDto dto)
    {
        var server = _mapper.Map<Server>(dto);

        _context.Servers.Add(server);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<ServerDto>(server);

        return CreatedAtAction(nameof(GetServer), new { id = server.ServerId }, result);
    }

    // PUT: api/servers/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateServer(int id, ServerDto dto)
    {
        if (id != dto.ServerId)
            return BadRequest("ID mismatch");

        var existing = await _context.Servers.FindAsync(id);
        if (existing == null)
            return NotFound();

        _mapper.Map(dto, existing);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/servers/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServer(int id)
    {
        var server = await _context.Servers.FindAsync(id);
        if (server == null)
            return NotFound();

        _context.Servers.Remove(server);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}