using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Microsoft.AspNetCore.Cors;

namespace projnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableCors("AllowAnyOrigin")]
    public class DestinationNaviresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DestinationNaviresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/DestinationNavires
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Destination_Navire>>> GetDestination_Navire()
        {
            if (_context.Destination_Navire == null)
            {
                return NotFound("Destination_Navire is null");
            }

            return await _context.Destination_Navire.ToListAsync();
        }

        // GET: api/DestinationNavires/5/1

        [HttpGet("{numFacture}/{nomNavire}")]
public async Task<ActionResult<Destination_Navire>> GetDestination_Navire(int numFacture, string nomNavire)
{
    var destination_Navire = await _context.Destination_Navire
        .Include(d => d.Navire).AsNoTracking()
        //.Include(d => d.Facture).AsNoTracking() //les détails du factures
        .FirstOrDefaultAsync(d => d.numFacture == numFacture && d.nomNavire == nomNavire);

    if (destination_Navire == null)
    {
        return NotFound();
    }

    return destination_Navire;
}


        // PUT: api/DestinationNavires/5/1
        [HttpPut("{numFacture}/{nomNavire}")]
        public async Task<IActionResult> PutDestination_Navire(int numFacture, string nomNavire, Destination_Navire destination_Navire)
        {
            if (numFacture != destination_Navire.numFacture || nomNavire != destination_Navire.nomNavire)
            {
                return BadRequest();
            }

            _context.Entry(destination_Navire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Destination_NavireExists(numFacture, nomNavire))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DestinationNavires
        [HttpPost]
        public async Task<ActionResult<Destination_Navire>> PostDestination_Navire(Destination_Navire destination_Navire)
        {
            if (_context.Destination_Navire != null)
            {
            _context.Destination_Navire.Add(destination_Navire);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDestination_Navire", new { numFacture = destination_Navire.numFacture, nomNavire = destination_Navire.nomNavire }, destination_Navire);
        }

        // DELETE: api/DestinationNavires/5/1
        [HttpDelete("{numFacture}/{nomNavire}")]
        public async Task<IActionResult> DeleteDestination_Navire(int numFacture, string nomNavire)
        {
            if (_context.Destination_Navire != null)
            {
            var destination_Navire = await _context.Destination_Navire.FindAsync(numFacture, nomNavire);
            
            if (destination_Navire == null)
            {
                return NotFound();
            }
            
            _context.Destination_Navire.Remove(destination_Navire);
            await _context.SaveChangesAsync();
            }
            return NoContent();
        }


        private bool Destination_NavireExists(int numFacture, string nomNavire)
        {
            return _context.Destination_Navire != null && _context.Destination_Navire.Any(e => e.numFacture == numFacture && e.nomNavire == nomNavire);
        }

        
    }
    
}
