using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Client
{
    [Key]
    [Required]
    [Column("code_client")]
    public int codeClient { get; set; }
    [Column("nom_client")]
    public string? nomClient { get; set; }

    [Column("adresse_client")]
    public string? adresseClient { get; set; }

    [Column("utilisateur_id")]
    public int utilisateurId { get; set; }
    
    [ForeignKey("UtilisateurId")]
    public virtual Utilisateur? Utilisateur { get; set; }
}