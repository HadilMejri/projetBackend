using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Libelle_marchandise
{
    
    [Key]
    [Column("designation")]
    public string? designation { get; set; }

    [Key]
    [Column("marque_code")]
    public string? marqueCode { get; set; }
    
    [Column("col_mtg")]
    public string? colMtg { get; set; }

    [Column("poids")]
    public double poids { get; set; }

    [ForeignKey("marqueCode")]
    public virtual Marque? Marque { get; set; }

    // Méthode pour convertir la valeur Poids en System.Single
public Single GetPoidsAsSingle()
{
    return Convert.ToSingle((double)poids);
}

}
