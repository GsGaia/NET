using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gaia.Domain.Entity;

[Table("Requestion")]
public class Requestion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long idRequestion { get; set; }
    
    //String
    public string title { get; set; }
    public string description { get; set; }
    public string unit { get; set; }
    
    //DataTime
    public DateTime RequestDate { get; set; }

    //Boolean
    public bool isActive { get; set; }
    
    //Relacionamentos
    public User User { get; set; }
    public Location location { get; set; }
    
}