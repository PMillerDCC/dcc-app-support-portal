using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeDevOps.Api.Dtos;
using SeDevOps.Data;
using SeDevOps.Data.Entities;

namespace SeDevOps.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public NotesController(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/notes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteDto>>> GetNotes()
    {
        var notes = await _context.Notes.ToListAsync();
        return Ok(_mapper.Map<IEnumerable<NoteDto>>(notes));
    }

    // GET: api/notes/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<NoteDto>> GetNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note == null)
            return NotFound();

        return Ok(_mapper.Map<NoteDto>(note));
    }

    // POST: api/notes
    [HttpPost]
    public async Task<ActionResult<NoteDto>> CreateNote(NoteDto dto)
    {
        // Validate foreign keys
        var appExists = await _context.Applications.AnyAsync(a => a.ApplicationId == dto.ApplicationId);
        if (!appExists)
            return BadRequest("Invalid ApplicationId");

        var userExists = await _context.Users.AnyAsync(u => u.UserId == dto.UserId);
        if (!userExists)
            return BadRequest("Invalid UserId");

        var note = _mapper.Map<Note>(dto);

        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        var result = _mapper.Map<NoteDto>(note);

        return CreatedAtAction(nameof(GetNote), new { id = note.NoteId }, result);
    }

    // PUT: api/notes/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(int id, NoteDto dto)
    {
        if (id != dto.NoteId)
            return BadRequest("ID mismatch");

        var existing = await _context.Notes.FindAsync(id);
        if (existing == null)
            return NotFound();

        // Validate foreign keys
        var appExists = await _context.Applications.AnyAsync(a => a.ApplicationId == dto.ApplicationId);
        if (!appExists)
            return BadRequest("Invalid ApplicationId");

        var userExists = await _context.Users.AnyAsync(u => u.UserId == dto.UserId);
        if (!userExists)
            return BadRequest("Invalid UserId");

        _mapper.Map(dto, existing);

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/notes/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);
        if (note == null)
            return NotFound();

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
