using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace projnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableCors("AllowAnyOrigin")]
    public class MarquesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MarquesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Méthode pour créer une marque
        [HttpPost]
        public async Task<IActionResult> CreateMarque([FromBody] Marque marque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Marque != null)
            {
                _context.Marque.Add(marque);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetMarque", new { marqueCode = marque.marqueCode }, marque);
            }
            else
            {
                return BadRequest("Marques is null");
            }
        }

        // GET: api/Marques
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marque>>> GetMarque()
        {
            if (_context.Marque == null)
            {
                return NotFound("Marque is null");
            }

            return await _context.Marque.ToListAsync();
        }

        // Méthode pour récupérer une marque
        [HttpGet("{marqueCode}")]
        public async Task<IActionResult> GetMarque(string marqueCode)
        {
            if (_context.Marque == null)
            {
                return NotFound("Marques is null");
            }

            var marque = await _context.Marque.FindAsync(marqueCode);

            if (marque == null)
            {
                return NotFound();
            }

            return Ok(marque);
        }

        // Méthode pour mettre à jour une marque
        [HttpPut("{marqueCode}")]
        public async Task<IActionResult> UpdateMarque(string marqueCode, [FromBody] Marque marque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (marqueCode != marque.marqueCode)
            {
                return BadRequest();
            }

            if (_context.Marque == null)
            {
                return NotFound("Marques is null");
            }

            var existingMarque = await _context.Marque.FindAsync(marqueCode);
            if (existingMarque == null)
            {
                return NotFound();
            }

            _context.Entry(existingMarque).CurrentValues.SetValues(marque);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Méthode pour supprimer une marque
        [HttpDelete("{marqueCode}")]
        public async Task<IActionResult> DeleteMarque(string marqueCode)
        {
            if (_context.Marque == null)
            {
                return NotFound("Marques is null");
            }

            var marque = await _context.Marque.FindAsync(marqueCode);

            if (marque == null)
            {
                return NotFound();
            }

            _context.Marque.Remove(marque);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
