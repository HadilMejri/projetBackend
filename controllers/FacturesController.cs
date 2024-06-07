/* This C# code snippet defines a controller class `FacturesController` that handles HTTP requests
related to managing invoices (factures). Here is a breakdown of what each part of the code does: */
using Microsoft.EntityFrameworkCore; 
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace projnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [EnableCors("AllowAnyOrigin")]
    public class FacturesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FacturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Méthode pour créer une Facture
        [HttpPost("addfac")]
        public async Task<IActionResult> CreateFacture([FromBody] Facture facture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Facture.Add(facture);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFacture), new { id = facture.numFacture }, facture);
        }


        // GET: api/Factures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facture>>> GetFacture()
        {
            if (_context.Facture == null)
            {
                return NotFound("Facture is null");
            }

            return await _context.Facture.ToListAsync();
        }
        // Méthode pour mettre à jour une Facture
        [HttpPut("putfac/{id}")]
        public async Task<IActionResult> UpdateFacture(int id, [FromBody] Facture facture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != facture.numFacture)
            {
                return BadRequest();
            }

            _context.Entry(facture).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Méthode pour supprimer une Facture
        [HttpDelete("deletefac/{id}")]
        public async Task<IActionResult> DeleteFacture(int id)
        {
            var facture = await _context.Facture.FindAsync(id);

            if (facture == null)
            {
                return NotFound();
            }

            _context.Facture.Remove(facture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Les détails de facture
        [HttpGet("{numeroFacture}")]
        public async Task<IActionResult> GetFactureDetails(int numeroFacture)
        {
            var factureDetails = await _context.Set<FactureDetails>()
            .FromSqlInterpolated($@"
                SELECT facture.num_facture, facture.date, facture.ville, facture.pays, facture.code_fiscal, facture.avance_euro, facture.destination_euro , facture.avance_millime, facture.destination_millime,
                client.code_client,
                navire.nom_navire,
                prestation.num, prestation.rubrique, prestation.base, prestation.taux, prestation.montant, prestation.type_rubrique,
                dossier.dossier_num, dossier.b_l_numero,
                marque.marque_code,
                libelle_marchandise.designation, libelle_marchandise.col_mtg, libelle_marchandise.poids,
                destination_navire.provenance, destination_navire.date_depart, destination_navire.destination
                FROM facture
                INNER JOIN client ON facture.code_client = client.code_client
                INNER JOIN navire ON facture.nom_navire = navire.nom_navire
                INNER JOIN dossier ON facture.dossier_num = dossier.dossier_num
                INNER JOIN marque ON dossier.dossier_num = marque.dossier_num
                INNER JOIN libelle_marchandise ON marque.marque_code = libelle_marchandise.marque_code
                INNER JOIN prestation ON facture.num_facture = prestation.num_facture
                INNER JOIN destination_navire ON facture.num_facture = destination_navire.num_facture
                        WHERE facture.num_facture = {numeroFacture}")
            .ToListAsync();

            if (factureDetails == null || factureDetails.Count == 0)
            {
                return NotFound();
            }
            return Ok(factureDetails);
        }
    }
}
