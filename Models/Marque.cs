using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Marque
{
    [Key]
    [Required]
    [Column("marque_code")]
    public string? marqueCode { get; set; }

    [Column("dossier_num")]
    public string? dossierNum { get; set; }

    [ForeignKey("dossierNum")]
    public virtual Dossier? Dossier { get; set; }

    
}