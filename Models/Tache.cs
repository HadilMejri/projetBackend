using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Tache
{
    [Key]
    [Required]
    [Column("sujet")]
    public string? sujet { get; set; }

    [Column("date_debut")]
    public DateTime dateDebut { get; set; }

    [Column("date_fin")]
    public DateTime dateFin { get; set; }

    [Column("statut")]
    public string? statut { get; set; }

    [Column("priorite")]
    public string? priorite { get; set; }

    [Column("details")]
    public string? details { get; set; }

    [Column("assigne_a")]
    public string? assigne_a { get; set; }

    [Column("utilisateur_id")]
    public int utilisateurId { get; set; }

    [ForeignKey("utilisateurId")]
    public virtual Utilisateur? Utilisateur { get; set; }
}
