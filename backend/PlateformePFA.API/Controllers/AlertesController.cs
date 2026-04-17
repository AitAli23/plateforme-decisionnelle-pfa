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
    public class AlertesController : ControllerBase
    {
        private readonly AppDbContext _context;
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
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}