using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class FactureDetails
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

    [Column("nom_navire")]
    public string? nomNavire { get; set; }

    [Column("num")]
    public string? numPrestation { get; set; }

    [Column("rubrique")]
    public string? rubrique { get; set; }

    [Column("base")]
    public string? Base { get; set; }

    [Column("taux")]
    public string? taux { get; set; }

    [Column("montant")]
    public string? montant { get; set; }

    [Column("type_rubrique")]
    public string? typeRubrique { get; set; }

    [Column("dossier_num")]
    public string? dossierNum { get; set; }

    [Column("b_l_numero")]
    public int blNumero { get; set; }

    [Column("marque_code")]
    public string? marqueCode { get; set; }

    [Column("designation")]
    public string? designation { get; set; }

    [Column("col_mtg")]
    public string? colMTG { get; set; }

    [Column("poids")]
    public double poids { get; set; }

    [Column("provenance")]
    public string? provenance { get; set; }

    [Column("date_depart")]
    public DateTime dateDepart { get; set; }

    [Column("destination")]
    public string? destination { get; set; }
    public Single GetPoidsAsSingle()
{
    return Convert.ToSingle((double)poids);
}
}







    
