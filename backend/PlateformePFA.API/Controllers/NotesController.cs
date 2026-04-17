using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlateformePFA.API.Data;
using PlateformePFA.API.Models;

namespace PlateformePFA.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly AppDbContext _context;
<<<<<<< HEAD

        public NotesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Notes
=======
        private readonly ILogger<NotesController> _logger;

        public NotesController(AppDbContext context, ILogger<NotesController> logger)
        {
            _context = context;
            _logger = logger;
        }

>>>>>>> 2c353432fb6f0eafc08dc93c51704438f7f4de6a
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            return await _context.Notes
                .Include(n => n.Etudiant)
                .Include(n => n.Module)
                .ToListAsync();
        }

<<<<<<< HEAD
        // GET: api/Notes/5
=======
>>>>>>> 2c353432fb6f0eafc08dc93c51704438f7f4de6a
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            var note = await _context.Notes
                .Include(n => n.Etudiant)
                .Include(n => n.Module)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (note == null) return NotFound();
<<<<<<< HEAD
            return note;
        }

        // POST: api/Notes
=======

            return note;
        }

        [HttpGet("etudiant/{etudiantId}")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotesByEtudiant(int etudiantId)
        {
            return await _context.Notes
                .Include(n => n.Etudiant)
                .Include(n => n.Module)
                .Where(n => n.EtudiantId == etudiantId)
                .ToListAsync();
        }

        [HttpGet("module/{moduleId}")]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotesByModule(int moduleId)
        {
            return await _context.Notes
                .Include(n => n.Etudiant)
                .Include(n => n.Module)
                .Where(n => n.ModuleId == moduleId)
                .ToListAsync();
        }

        [Authorize(Roles = "Admin,Enseignant")]
>>>>>>> 2c353432fb6f0eafc08dc93c51704438f7f4de6a
        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
<<<<<<< HEAD
            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
        }

        // PUT: api/Notes/5
=======

            _logger.LogInformation("Note créée : Id={NoteId}, EtudiantId={EtudiantId}, ModuleId={ModuleId}", note.Id, note.EtudiantId, note.ModuleId);

            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
        }

        [Authorize(Roles = "Admin,Enseignant")]
>>>>>>> 2c353432fb6f0eafc08dc93c51704438f7f4de6a
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, Note note)
        {
            if (id != note.Id) return BadRequest();

            _context.Entry(note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
<<<<<<< HEAD
                if (!_context.Notes.Any(e => e.Id == id)) return NotFound();
=======
                if (!_context.Notes.Any(n => n.Id == id)) return NotFound();
>>>>>>> 2c353432fb6f0eafc08dc93c51704438f7f4de6a
                else throw;
            }

            return NoContent();
        }

<<<<<<< HEAD
        // DELETE: api/Notes/5
=======
        [Authorize(Roles = "Admin")]
>>>>>>> 2c353432fb6f0eafc08dc93c51704438f7f4de6a
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) return NotFound();

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
<<<<<<< HEAD
            return NoContent();
        }
    }
}
=======

            _logger.LogInformation("Note supprimée : Id={NoteId}, EtudiantId={EtudiantId}, ModuleId={ModuleId}", note.Id, note.EtudiantId, note.ModuleId);

            return NoContent();
        }
    }
}
>>>>>>> 2c353432fb6f0eafc08dc93c51704438f7f4de6a
