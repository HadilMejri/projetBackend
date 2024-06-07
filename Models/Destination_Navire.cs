using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


public class Destination_Navire
{
    
    [Key]
    [Column("nom_navire")]
    public string? nomNavire { get; set; } 

    [Key]
    [Column("num_facture")]
    public int numFacture { get; set; }

    [Column("provenance")]
    public string? provenance { get; set; }

    [Column("date_depart")]
    public DateTime dateDepart { get; set; }

    [Column("destination")]
    public string? destination { get; set; }

    [ForeignKey("nomNavire")]
    [JsonIgnore]
    public virtual Navire? Navire { get; set; }

    [ForeignKey("numFacture")]
    public virtual Facture? Facture { get; set; }

}
