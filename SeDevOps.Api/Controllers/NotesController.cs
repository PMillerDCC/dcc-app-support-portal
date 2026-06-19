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
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetAll()
        {
            var notes = await _context.Notes
                .Include(n => n.User)
                .Include(n => n.Application)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<NoteDto>>(notes));
        }

        // GET: api/notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDto>> Get(int id)
        {
            var note = await _context.Notes
                .Include(n => n.User)
                .Include(n => n.Application)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (note == null)
                return NotFound();

            return Ok(_mapper.Map<NoteDto>(note));
        }

        // GET: api/notes/by-application/3
        [HttpGet("by-application/{appId}")]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetByApplication(int appId)
        {
            var notes = await _context.Notes
                .Where(n => n.ApplicationId == appId)
                .Include(n => n.User)
                .Include(n => n.Application)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<NoteDto>>(notes));
        }

        // POST: api/notes
        [HttpPost]
        public async Task<ActionResult<NoteDto>> Create(NoteDto dto)
        {
            var note = _mapper.Map<Note>(dto);
            note.CreatedAt = DateTime.UtcNow;

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = note.Id }, _mapper.Map<NoteDto>(note));
        }

        // PUT: api/notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NoteDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var note = await _context.Notes.FindAsync(id);
            if (note == null)
                return NotFound();

            _mapper.Map(dto, note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
                return NotFound();

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}