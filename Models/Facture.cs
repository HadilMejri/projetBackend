using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Facture
{
    [Key]
    [Required]
    [Column("num_facture")]
    public int numFacture { get; set; }

    [Column("date")]
    public DateTime date { get; set; }

    [Column("ville")]
    public string? ville { get; set; }

    [Column("pays")]
    public string? pays { get; set; }

    [Column("avance_euro")]
    public string? avanceEuro { get; set; }

    [Column("destination_euro")]
    public string? destinationEuro { get; set; }

    [Column("avance_millime")]
    public string? avanceMillime { get; set; }

    [Column("destination_millime")]
    public string? destinationMillime { get; set; }

    [Column("code_fiscal")]
    public string? codeFiscal { get; set; }

    [Column("code_client")]
    public int codeClient { get; set; }

    [Column("dossier_num")]
    public string? dossierNum { get; set; }

    [Column("nom_navire")]
    public string? nomNavire { get; set; }

    [Column("utilisateur_id")]
    public int utilisateurId { get; set; }
    
    [ForeignKey("codeClient")]
    public virtual Client? Client { get; set; }

    [ForeignKey("dossierNum")]
    public virtual Dossier? Dossier { get; set; }

    [ForeignKey("nomNavire")]
    public virtual Navire? Navire { get; set; }

    [ForeignKey("utilisateurId")]
    public virtual Utilisateur? Utilisateur { get; set; }
}