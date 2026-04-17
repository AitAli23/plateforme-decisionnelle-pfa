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
    public class PresencesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PresencesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Presences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Presence>>> GetPresences()
        {
            return await _context.Presences
                .Include(p => p.Etudiant)
                .Include(p => p.Module)
                .ToListAsync();
        }

        // GET: api/Presences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Presence>> GetPresence(int id)
        {
            var presence = await _context.Presences
                .Include(p => p.Etudiant)
                .Include(p => p.Module)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (presence == null) return NotFound();
            return presence;
        }

        // POST: api/Presences
        [HttpPost]
        public async Task<ActionResult<Presence>> PostPresence(Presence presence)
        {
            _context.Presences.Add(presence);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPresence), new { id = presence.Id }, presence);
        }

        // PUT: api/Presences/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPresence(int id, Presence presence)
        {
            if (id != presence.Id) return BadRequest();

            _context.Entry(presence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Presences.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/Presences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePresence(int id)
        {
            var presence = await _context.Presences.FindAsync(id);
            if (presence == null) return NotFound();

            _context.Presences.Remove(presence);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}