using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeDevOps.Data;
using SeDevOps.Api.Dtos;
using SeDevOps.Data.Entities;

namespace SeDevOps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class  ApplicationsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ApplicationsController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/applications
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetApplications()
    {
        var apps = await _context.Applications.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<ApplicationDto>>(apps));
    }

    // GET: api/applications/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ApplicationDto>> GetApplication(int id)
    {
        var app = await _context.Applications.FindAsync(id);

        if (app == null)
            return NotFound();

        return Ok(_mapper.Map<ApplicationDto>(app));
    }

    // POST: api/applications
    [HttpPost]
    public async Task<ActionResult<ApplicationDto>> CreateApplication(ApplicationDto dto)
    {
        var app = _mapper.Map<Application>(dto);

        _context.Applications.Add(app);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<ApplicationDto>(app);

        return CreatedAtAction(nameof(GetApplication), new { id = app.ApplicationId }, result);
    }

    // PUT: api/applications/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateApplication(int id, ApplicationDto dto)
    {
        if (id != dto.ApplicationId)
            return BadRequest("ID mismatch");

        var existing = await _context.Applications.FindAsync(id);
        if (existing == null)
            return NotFound();

        _mapper.Map(dto, existing);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/applications/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApplication(int id)
    {
        var app = await _context.Applications.FindAsync(id);
        if (app == null)
            return NotFound();

        _context.Applications.Remove(app);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
