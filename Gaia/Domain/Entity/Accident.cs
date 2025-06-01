using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gaia.Domain.Enum;

namespace Gaia.Domain.Entity;
[Table("Accident")]
public class Accident
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long idAccident { get; set; }
    
    //DataTime
    public DateTime dateAccidentEnd { get; set; }
    public DateTime dateAccidentStart { get; set; }
    
    //Enum
    public TypeSeverity  typeSeverity { get; set; }
    public TypeAccident typeAccident { get; set; }
    
    //Relacionamento
    public long idLocation { get; set; }
    
    [ForeignKey("idLocation")]
    public Location location { get; set; }
}