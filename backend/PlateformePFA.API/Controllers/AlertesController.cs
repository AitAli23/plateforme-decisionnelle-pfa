using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlateformePFA.API.Data;
using PlateformePFA.API.Models;

namespace PlateformePFA.API.Controllers
{
<<<<<<< HEAD
    [Authorize]
=======
    [Authorize(Roles = "Admin,Responsable")]
>>>>>>> 2c353432fb6f0eafc08dc93c51704438f7f4de6a
    [Route("api/[controller]")]
    [ApiController]
    public class AlertesController : ControllerBase
    {
        private readonly AppDbContext _context;
<<<<<<< HEAD
        public AlertesController(AppDbContext context) { _context = context; }

        // GET: api/Alertes (Pour le tableau de bord du Responsable)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alerte>>> GetAlertes()
        {
            return await _context.Alertes
                .Include(a => a.Etudiant)
                .OrderByDescending(a => a.DateGeneration)
                .ToListAsync();
        }

        // POST: api/Alertes (Sera appelé quand le score de risque > 60%)
        [HttpPost]
        public async Task<ActionResult<Alerte>> PostAlerte(Alerte alerte)
        {
            _context.Alertes.Add(alerte);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAlertes), new { id = alerte.Id }, alerte);
        }

        // PUT: api/Alertes/5 (Pour marquer une alerte comme "Traitée")
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlerte(int id, Alerte alerte)
        {
            if (id != alerte.Id) return BadRequest();
            _context.Entry(alerte).State = EntityState.Modified;
=======
        private readonly ILogger<AlertesController> _logger;

        public AlertesController(AppDbContext context, ILogger<AlertesController> logger)
        {
            _context = context;
            _logger  = logger;
        }

        // GET: api/alertes?resolue=false
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alerte>>> GetAlertes([FromQuery] bool? resolue = null)
        {
            var query = _context.Alertes.Include(a => a.Etudiant).AsQueryable();

            if (resolue.HasValue)
                query = query.Where(a => a.Resolue == resolue.Value);

            return await query.OrderByDescending(a => a.CreeLe).ToListAsync();
        }

        // GET: api/alertes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alerte>> GetAlerte(int id)
        {
            var alerte = await _context.Alertes
                .Include(a => a.Etudiant)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (alerte == null) return NotFound(new { message = "Alerte introuvable." });
            return alerte;
        }

        // GET: api/alertes/etudiant/5
        [HttpGet("etudiant/{etudiantId}")]
        public async Task<ActionResult<IEnumerable<Alerte>>> GetAlertesByEtudiant(int etudiantId)
        {
            return await _context.Alertes
                .Include(a => a.Etudiant)
                .Where(a => a.EtudiantId == etudiantId)
                .OrderByDescending(a => a.CreeLe)
                .ToListAsync();
        }

        // POST: api/alertes
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Alerte>> PostAlerte(Alerte alerte)
        {
            alerte.CreeLe  = DateTime.UtcNow;
            alerte.Resolue = false;

            _context.Alertes.Add(alerte);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Alerte créée : Id={Id}, Type={Type}, Niveau={Niveau}, EtudiantId={EtudiantId}",
                alerte.Id, alerte.Type, alerte.Niveau, alerte.EtudiantId);

            return CreatedAtAction(nameof(GetAlerte), new { id = alerte.Id }, alerte);
        }

        // PATCH: api/alertes/5/resoudre
        [HttpPatch("{id}/resoudre")]
        public async Task<IActionResult> ResoudreAlerte(int id)
        {
            var alerte = await _context.Alertes.FindAsync(id);
            if (alerte == null) return NotFound(new { message = "Alerte introuvable." });

            alerte.Resolue    = true;
            alerte.ResolueeLe = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Alerte résolue : Id={Id}, EtudiantId={EtudiantId}", alerte.Id, alerte.EtudiantId);
            return NoContent();
        }

        // DELETE: api/alertes/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlerte(int id)
        {
            var alerte = await _context.Alertes.FindAsync(id);
            if (alerte == null) return NotFound(new { message = "Alerte introuvable." });

            _context.Alertes.Remove(alerte);
>>>>>>> 2c353432fb6f0eafc08dc93c51704438f7f4de6a
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 2c353432fb6f0eafc08dc93c51704438f7f4de6a
