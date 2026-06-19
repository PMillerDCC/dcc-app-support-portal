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
    public class ServersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ServersController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServerDto>>> GetAll()
        {
            var servers = await _context.Servers.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ServerDto>>(servers));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServerDto>> Get(int id)
        {
            var server = await _context.Servers.FindAsync(id);
            if (server == null) return NotFound();
            return Ok(_mapper.Map<ServerDto>(server));
        }

        [HttpPost]
        public async Task<ActionResult<ServerDto>> Create(ServerDto dto)
        {
            var server = _mapper.Map<Server>(dto);
            _context.Servers.Add(server);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = server.Id }, _mapper.Map<ServerDto>(server));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ServerDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var server = await _context.Servers.FindAsync(id);
            if (server == null) return NotFound();

            _mapper.Map(dto, server);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var server = await _context.Servers
                .Include(s => s.Applications)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (server == null)
                return NotFound();

            if (server.Applications.Any())
                return BadRequest("Cannot delete a server that has applications assigned.");

            _context.Servers.Remove(server);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
