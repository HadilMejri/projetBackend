using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Linq;
using System.Threading.Tasks;

namespace projnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableCors("AllowAnyOrigin")]
    public class DossiersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DossiersController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: api/Dossiers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dossier>>> GetDossier()
        {
            if (_context.Dossier == null)
            {
                return NotFound("Dossier is null");
            }

            return await _context.Dossier.ToListAsync();
        }

        // GET: api/Dossiers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dossier>> GetDossier(string id)
        {
            if (_context.Dossier == null)
            {
                return NotFound("Dossiers is null");
            }

            var dossier = await _context.Dossier.FirstOrDefaultAsync(d => d.dossierNum == id);

            if (dossier == null)
            {
                return NotFound();
            }

            return dossier;
        }

        // PUT: api/Dossiers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDossier(string id, Dossier dossier)
        {
            if (id != dossier.dossierNum)
            {
                return BadRequest();
            }

            if (_context.Dossier == null)
            {
                return NotFound("Dossiers is null");
            }

            var existingDossier = await _context.Dossier.FirstOrDefaultAsync(d => d.dossierNum == id);

            if (existingDossier == null)
            {
                return NotFound();
            }

            _context.Entry(existingDossier).CurrentValues.SetValues(dossier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Dossiers
        [HttpPost]
        public async Task<ActionResult<Dossier>> PostDossier(Dossier dossier)
        {
            if (_context.Dossier == null)
            {
                return NotFound("Dossiers is null");
            }

            _context.Dossier.Add(dossier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDossier", new { id = dossier.dossierNum }, dossier);
        }

        // DELETE: api/Dossiers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDossier(string id)
        {
            if (_context.Dossier == null)
            {
                return NotFound("Dossiers is null");
            }

            var dossier = await _context.Dossier.FirstOrDefaultAsync(d => d.dossierNum == id);

            if (dossier == null)
            {
                return NotFound();
            }

            _context.Dossier.Remove(dossier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DossierExists(string id)
        {
            return (_context.Dossier?.Any(d => d.dossierNum == id)).GetValueOrDefault();
        }
    }
}
