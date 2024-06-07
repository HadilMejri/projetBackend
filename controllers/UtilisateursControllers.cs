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
    // [EnableCors("AllowAnyOrigin")]
    public class UtilisateursController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UtilisateursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Utilisateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateurs()
        {
            if (_context.Utilisateur == null)
            {
                return NotFound("Utilisateurs is null");
            }

            return await _context.Utilisateur.ToListAsync();
        }

        // GET: api/Utilisateurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateur(int id)
        {
            if (_context.Utilisateur == null)
            {
                return NotFound("Utilisateurs is null");
            }

            var utilisateur = await _context.Utilisateur.FirstOrDefaultAsync(u => u.utilisateur_id == id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }

        // PUT: api/Utilisateurs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateur(int id, Utilisateur utilisateur)
        {
            if (id != utilisateur.utilisateur_id)
            {
                return BadRequest();
            }

            if (_context.Utilisateur == null)
            {
                return NotFound("Utilisateurs is null");
            }

            var existingUtilisateur = await _context.Utilisateur.FirstOrDefaultAsync(u => u.utilisateur_id == id);

            if (existingUtilisateur == null)
            {
                return NotFound();
            }

            _context.Entry(existingUtilisateur).CurrentValues.SetValues(utilisateur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Utilisateurs
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> PostUtilisateur(Utilisateur utilisateur)
        {
            if (_context.Utilisateur == null)
            {
                return NotFound("Utilisateurs is null");
            }

            _context.Utilisateur.Add(utilisateur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilisateur", new { id = utilisateur.utilisateur_id }, utilisateur);
        }

        // GET: api/Utilisateurs/{id}/Taches
        [HttpGet("{id}/Taches")]
        public async Task<ActionResult<IEnumerable<Tache>>> GetTachesByUtilisateur(int id)
        {
            var utilisateur = await _context.Utilisateur.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound($"Utilisateur with ID {id} not found.");
            }

        var taches = await _context.Tache.Where(t => t.utilisateurId == id).ToListAsync();
        return taches;
        }


        // DELETE: api/Utilisateurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            if (_context.Utilisateur == null)
            {
                return NotFound("Utilisateurs is null");
            }

            var utilisateur = await _context.Utilisateur.FirstOrDefaultAsync(u => u.utilisateur_id == id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            _context.Utilisateur.Remove(utilisateur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UtilisateurExists(int id)
        {
            return (_context.Utilisateur?.Any(u => u.utilisateur_id == id)).GetValueOrDefault();
        }
    }
}
