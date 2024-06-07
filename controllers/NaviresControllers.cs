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
    public class NaviresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NaviresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Navires
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Navire>>> GetNavire()
        {
            if (_context.Navire == null)
            {
                return NotFound("Navire is null");
            }

            return await _context.Navire.ToListAsync();
        }

        // GET: api/Navires/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Navire>> GetNavire(string id)
        {
            if (_context.Navire == null)
            {
                return NotFound("Navires is null");
            }

            var navire = await _context.Navire.FirstOrDefaultAsync(n => n.nomNavire == id);

            if (navire == null)
            {
                return NotFound();
            }

            return navire;
        }

        // PUT: api/Navires/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNavire(string id, Navire navire)
        {
            if (id != navire.nomNavire)
            {
                return BadRequest();
            }

            if (_context.Navire == null)
            {
                return NotFound("Navires is null");
            }

            var existingNavire = await _context.Navire.FirstOrDefaultAsync(n => n.nomNavire == id);

            if (existingNavire == null)
            {
                return NotFound();
            }

            _context.Entry(existingNavire).CurrentValues.SetValues(navire);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Navires
        [HttpPost]
        public async Task<ActionResult<Navire>> PostNavire(Navire navire)
        {
            if (_context.Navire == null)
            {
                return NotFound("Navires is null");
            }

            _context.Navire.Add(navire);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNavire", new { id = navire.nomNavire }, navire);
        }

        // DELETE: api/Navires/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNavire(string id)
        {
            if (_context.Navire == null)
            {
                return NotFound("Navires is null");
            }

            var navire = await _context.Navire.FirstOrDefaultAsync(n => n.nomNavire == id);

            if (navire == null)
            {
                return NotFound();
            }

            _context.Navire.Remove(navire);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NavireExists(string id)
        {
            return (_context.Navire?.Any(e => e.nomNavire == id)).GetValueOrDefault();
        }
    }
}
