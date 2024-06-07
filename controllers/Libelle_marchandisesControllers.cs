using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace projnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableCors("AllowAnyOrigin")]
    public class LibelleMarchandisesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LibelleMarchandisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Méthode pour créer une libellé marchandise
        [HttpPost]
        public async Task<IActionResult> CreateLibelleMarchandise([FromBody] Libelle_marchandise libelleMarchandise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Libelle_marchandise != null)
            {
                _context.Libelle_marchandise.Add(libelleMarchandise);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetLibelleMarchandise", new { designation = libelleMarchandise.designation, marqueCode = libelleMarchandise.marqueCode }, libelleMarchandise);
            }
            else
            {
                return BadRequest("LibelleMarchandises is null");
            }
        }

        // Méthode pour récupérer une libellé marchandise
        [HttpGet("{designation}/{marqueCode}")]
        public async Task<IActionResult> GetLibelleMarchandise(string designation, string marqueCode)
        {
            if (_context.Libelle_marchandise == null)
            {
                return NotFound("LibelleMarchandises is null");
            }

            var libelleMarchandise = await _context.Libelle_marchandise.FindAsync(designation, marqueCode);

            if (libelleMarchandise == null)
            {
                return NotFound();
            }

            return Ok(libelleMarchandise);
        }

        // GET: api/Libelle_marchandise
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libelle_marchandise>>> GetLibelleMarchandise()
        {
            if (_context.Libelle_marchandise == null)
            {
                return NotFound("LibelleMarchandises is null");
            }

            return await _context.Libelle_marchandise.ToListAsync();
        }

        // Méthode pour mettre à jour une libellé marchandise
        [HttpPut("{designation}/{marqueCode}")]
        public async Task<IActionResult> UpdateLibelleMarchandise(string designation, string marqueCode, [FromBody] Libelle_marchandise libelleMarchandise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (designation != libelleMarchandise.designation || marqueCode != libelleMarchandise.marqueCode)
            {
                return BadRequest();
            }

            if (_context.Libelle_marchandise == null)
            {
                return NotFound("LibelleMarchandises is null");
            }

            var existingLibelleMarchandise = await _context.Libelle_marchandise.FindAsync(designation, marqueCode);
            if (existingLibelleMarchandise == null)
            {
                return NotFound();
            }

            _context.Entry(existingLibelleMarchandise).CurrentValues.SetValues(libelleMarchandise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Méthode pour supprimer une libellé marchandise
        [HttpDelete("{designation}/{marqueCode}")]
        public async Task<IActionResult> DeleteLibelleMarchandise(string designation, string marqueCode)
        {
            if (_context.Libelle_marchandise == null)
            {
                return NotFound("LibelleMarchandises is null");
            }

            var libelleMarchandise = await _context.Libelle_marchandise.FindAsync(designation, marqueCode);

            if (libelleMarchandise == null)
            {
                return NotFound();
            }

            _context.Libelle_marchandise.Remove(libelleMarchandise);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
