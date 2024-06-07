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
    public class PrestationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PrestationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Méthode pour créer une prestation
        [HttpPost]
        public async Task<IActionResult> CreatePrestation([FromBody] Prestation prestation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Prestation != null)
            {
                _context.Prestation.Add(prestation);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPrestation), new { numFacture = prestation.numFacture, num = prestation.num }, prestation);
            }
            else
            {
                return BadRequest("Prestations is null");
            }
        }

        // Méthode pour récupérer une prestation
        [HttpGet("{numFacture}/{num}")]
        public async Task<IActionResult> GetPrestation(int numFacture, string num)
        {
            if (_context.Prestation != null)
            {
                var prestation = await _context.Prestation.FindAsync(numFacture, num);

                if (prestation == null)
                {
                    return NotFound();
                }

                return Ok(prestation);
            }
            else
            {
                return BadRequest("Prestations is null");
            }
        }

        // GET: api/Prestations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestation>>> GetPrestation()
        {
            if (_context.Prestation == null)
            {
                return NotFound("Prestation is null");
            }

            return await _context.Prestation.ToListAsync();
        }

        // Méthode pour mettre à jour une prestation
        [HttpPut("{numFacture}/{num}")]
        public async Task<IActionResult> UpdatePrestation(int numFacture, string num, [FromBody] Prestation prestation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Prestation != null)
            {
                if (numFacture != prestation.numFacture || num != prestation.num)
                {
                    return BadRequest();
                }

                _context.Entry(prestation).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            else
            {
                return BadRequest("Prestations is null");
            }
        }

        // Méthode pour supprimer une prestation
        [HttpDelete("{numFacture}/{num}")]
        public async Task<IActionResult> DeletePrestation(int numFacture, string num)
        {
            if (_context.Prestation != null)
            {
                var prestation = await _context.Prestation.FindAsync(numFacture, num);
                if (prestation == null)
                {
                    return NotFound();
                }

                _context.Prestation.Remove(prestation);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            else
            {
                return BadRequest("Prestations is null");
            }
        }
    }
}
