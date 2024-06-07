using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Dossier
{
    [Key]
    [Required]
    [Column("dossier_num")]
    public string? dossierNum { get; set; }

    [Column("b_l_numero")]
    public int blNumero { get; set; }

    [Column("utilisateur_id")]
    public int utilisateurId { get; set; }
    
    [ForeignKey("utilisateurId")]
    public virtual Utilisateur? Utilisateur { get; set; }

}