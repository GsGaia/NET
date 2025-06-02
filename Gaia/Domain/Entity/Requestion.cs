using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gaia.Domain.Entity;

[Table("Requestion")]
public class Requestion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IdRequestion { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public string Unit { get; set; }

    public DateTime RequestDate { get; set; }

    public bool IsActive { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }

    public long LocationId { get; set; }
    public Location Location { get; set; }
}
