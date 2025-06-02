using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gaia.Domain.Enum;

namespace Gaia.Domain.Entity;
[Table("Accident")]
public class Accident
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IdAccident { get; set; }

    public DateTime DateAccidentStart { get; set; }
    public DateTime DateAccidentEnd { get; set; }

    public TypeSeverity TypeSeverity { get; set; }
    public TypeAccident TypeAccident { get; set; }

    public long LocationId { get; set; }

    [ForeignKey("LocationId")]
    public Location Location { get; set; }
}
