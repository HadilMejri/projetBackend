using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Navire
{
    [Key]
    [Required]
    [Column("nom_navire")]
    public string? nomNavire { get; set; }


    [Column("utilisateur_id")]
    public int utilisateurId { get; set; }
    

    [ForeignKey("utilisateurId")]
    public virtual Utilisateur? Utilisateur { get; set; }

    // Ajoutez la propriété de navigation Factures ici
    public virtual ICollection<Facture> Factures { get; set; } = new HashSet<Facture>();
}