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
    public class ApplicationsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ApplicationsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetAll()
        {
            var apps = await _context.Applications
                .Include(a => a.Server)
                .ToListAsync();

            var dtos = apps.Select(a => new ApplicationDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                ServerId = a.ServerId,
                ServerName = a.Server.Name
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDto>> Get(int id)
        {
            var app = await _context.Applications
                .Include(a => a.Server)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (app == null)
                return NotFound();

            var dto = new ApplicationDto
            {
                Id = app.Id,
                Name = app.Name,
                Description = app.Description,
                ServerId = app.ServerId,
                ServerName = app.Server?.Name
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationDto>> Create(ApplicationCreateDto dto)
        {
            var app = _mapper.Map<Application>(dto);
            _context.Applications.Add(app);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<ApplicationDto>(app);
            return CreatedAtAction(nameof(Get), new { id = app.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ApplicationDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var app = await _context.Applications.FindAsync(id);
            if (app == null) return NotFound();

            _mapper.Map(dto, app);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var app = await _context.Applications.FindAsync(id);
            if (app == null) return NotFound();

            _context.Applications.Remove(app);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}