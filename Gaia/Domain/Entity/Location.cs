using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gaia.Domain.Enum;

namespace Gaia.Domain.Entity;

[Table("Location")]
public class Location
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IdLocation { get; set; }

    public string City { get; set; }

    public DateTime StartAccident { get; set; }
    public DateTime EndAccident { get; set; }

    public TypeStatusLocation Status { get; set; }
    public TypeStation Station { get; set; }

    public bool Active { get; set; }

    public ICollection<Requestion> Requestions { get; set; } = new List<Requestion>();

    public ICollection<Accident> Accidents { get; set; } = new List<Accident>();
}
