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
    public class TachesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TachesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Méthode pour créer une tâche
        [HttpPost]
public async Task<IActionResult> CreateTache([FromBody] Tache tache)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(ModelState);
    }

    // Log for debugging
    Console.WriteLine($"Creating task with sujet: {tache.sujet} and utilisateur_id: {tache.utilisateurId}");

    var utilisateurExiste = _context.Utilisateur.Any(u => u.utilisateur_id == tache.utilisateurId);
    if (!utilisateurExiste)
    {
        return BadRequest($"Utilisateur with ID {tache.utilisateurId} does not exist.");
    }

    if (_context.Tache != null)
    {
        try
        {
            _context.Tache.Add(tache);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTache", new { id = tache.sujet }, tache);
        }
        catch (Exception ex)
        {
            // Log the exception for further investigation
            Console.WriteLine($"An error occurred: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
    else
    {
        return BadRequest("Taches is null");
    }
}


        // Méthode pour récupérer une tâche
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTache(string id)
        {
            if (_context.Tache == null)
            {
                return NotFound("Taches is null");
            }

            var tache = await _context.Tache.FindAsync(id);

            if (tache == null)
            {
                return NotFound();
            }

            return Ok(tache);
        }

        // GET: api/Taches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tache>>> GetTache()
        {
            if (_context.Tache == null)
            {
                return NotFound("Tache is null");
            }

            return await _context.Tache.ToListAsync();
        }

        // Méthode pour mettre à jour une tâche
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTache(string id, [FromBody] Tache tache)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tache.sujet)

            {
                return BadRequest();
            }

            if (_context.Tache == null)
            {
                return NotFound("Taches is null");
            }

            var existingTache = await _context.Tache.FindAsync(id);
            if (existingTache == null)
            {
                return NotFound();
            }

            _context.Entry(existingTache).CurrentValues.SetValues(tache);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Méthode pour supprimer une tâche
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTache(int id)
        {
            if (_context.Tache == null)
            {
                return NotFound("Taches is null");
            }

            var tache = await _context.Tache.FindAsync(id);

            if (tache == null)
            {
                return NotFound();
            }

            _context.Tache.Remove(tache);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
