using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Prestation
{
    [Key]
    [Column("num_facture")]
    public int numFacture { get; set; }

    [Key]
    [Column("num")]
    public string? num { get; set; }

    [Column("rubrique")]
    public string? rubrique { get; set; }

    [Column("base")]
    public string? Base { get; set; }

    [Column("taux")]
    public string? taux { get; set; }

    [Column("montant")]
    public string? montant { get; set; }

    [Column("type_rubrique")]
    [MaxLength(1)]
    public string? typeRubrique { get; set; }

    [ForeignKey("numFacture")]
    public virtual Facture? Facture { get; set; }
}
