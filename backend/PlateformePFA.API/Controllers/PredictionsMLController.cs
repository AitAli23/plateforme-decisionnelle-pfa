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
    public class PredictionsMLController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PredictionsMLController(AppDbContext context) { _context = context; }

        // GET: api/PredictionsML/Etudiant/5 (Historique des prédictions pour un étudiant)
        [HttpGet("Etudiant/{etudiantId}")]
        public async Task<ActionResult<IEnumerable<PredictionML>>> GetPredictionsByEtudiant(int etudiantId)
        {
            return await _context.PredictionsML
                .Where(p => p.EtudiantId == etudiantId)
                .OrderByDescending(p => p.DatePrediction)
                .ToListAsync();
        }

        // POST: api/PredictionsML (Appelé par le microservice Python)
        [HttpPost]
        public async Task<ActionResult<PredictionML>> PostPrediction(PredictionML prediction)
        {
            _context.PredictionsML.Add(prediction);
            await _context.SaveChangesAsync();
            return Ok(prediction);
        }
    }
}