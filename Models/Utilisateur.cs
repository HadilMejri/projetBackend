using System.ComponentModel.DataAnnotations;

public class Utilisateur
{
    [Key]
    [Required]
    public int utilisateur_id { get; set; }
    public string? nom { get; set; }
    public string? prenom { get; set; }
    public string? adresse_mail { get; set; }
    public string? mot_de_passe { get; set; }
    public DateTime? date_naissance { get; set; }
    public string? departement { get; set; }
    public string? position { get; set; }
    public string? adresse { get; set; }
    public string? telephone { get; set; }
    public string? statut { get; set; }
    
}