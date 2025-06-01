using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gaia.Domain.Enum;

namespace Gaia.Domain.Entity;
[Table("Location")]
public class Location
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long idLocation { get; set; }
    
    //String
    public string city { get; set; }
    
    //DataTime
    public DateTime startAccident { get; set; }
    public DateTime endAccident { get; set; }
    
    //Enums
    public TypeStatusLocation  status { get; set; }
    public TypeStation station { get; set; }
    
    //Boolean
    public bool active { get; set; }
    
    //Relacionamento
    public ICollection<Requestion> requestions { get; set; } = new List<Requestion>();
    public Accident accident {get; set;}
}