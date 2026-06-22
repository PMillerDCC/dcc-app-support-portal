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
        public async Task<ActionResult<ApplicationDto>> GetById(int id)
        {
            var app = await _context.Applications
                .Include(a => a.Server)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (app == null)
                return NotFound();

            var dto = _mapper.Map<ApplicationDto>(app);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var application = _mapper.Map<Application>(dto);

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<ApplicationDto>(application);

            return CreatedAtAction(
                nameof(GetById),
                new { id = application.Id },
                resultDto
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ApplicationUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.Id)
                return BadRequest();

            var app = await _context.Applications.FindAsync(id);
            if (app == null)
                return NotFound();

            // Map only the fields that are allowed to change
            app.Name = dto.Name;
            app.Description = dto.Description;
            app.ServerId = dto.ServerId;

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