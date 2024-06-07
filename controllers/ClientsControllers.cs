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
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Méthode pour créer un Client
        [HttpPost]
        public async Task<IActionResult> CreateClient ([FromBody] Client Client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Client.nomClient != null)
            {
                _context.Client.Add(Client);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetClient", new { id = Client.codeClient }, Client);
            }
            else
            {
                if (_context.Client != null)
                {
                    _context.Client.Add(Client);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetClient", new { id = Client.codeClient }, Client);
                }
                else
                {
                    return BadRequest("Clients is null");
                }
            }

            
        }
        
        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            if (_context.Client == null)
            {
                return NotFound("Clients is null");
            }

            return await _context.Client.ToListAsync();
        }

        //Méthode pour récupérer un client
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            if (_context.Client == null)
            {
                return NotFound("Clients is null");
            }

            var Client = await _context.Client.FindAsync(id);

            if (Client == null)
            {
                return NotFound();
            }

            return Ok(Client);
        }

        
        //Méthode pour mettre à jour un Client
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.codeClient)
            {
                return BadRequest();
            }

            if (_context.Client == null)
            {
                return NotFound("Clients is null");
            }

                var existingClient = await _context.Client.FindAsync(id);
            if (existingClient == null)
            {
                return NotFound();
            }

            _context.Entry(existingClient).CurrentValues.SetValues(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        

        //Méthode pour supprimer un Client
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (_context.Client == null)
            {
                return NotFound("Clients is null");
            }

            var Client = await _context.Client.FindAsync(id);

            if (Client == null)
            {
                return NotFound();
            }

            _context.Client.Remove(Client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        private bool ClientExists(int id)
        {
                return (_context.Client?.Any(e => e.codeClient == id)).GetValueOrDefault();
        }



    }
}
