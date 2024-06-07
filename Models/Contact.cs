using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Contact
{
    
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int id { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? position { get; set; }
    public string? address { get; set; }
    public string? phone { get; set; }
    public string? email { get; set; }
   public byte[]? image { get; set; }
    
}