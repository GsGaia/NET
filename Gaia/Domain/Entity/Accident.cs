using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Gaia.Domain.Enum;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Gaia.Domain.Entity
{

    [Table("ACCIDENT")]
    public class Accident
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdAccident { get; set; }

        public DateTime DateAccidentStart { get; set; }
        public DateTime DateAccidentEnd { get; set; }

        public TypeSeverity TypeSeverity { get; set; }
        public TypeAccident TypeAccident { get; set; }

        [ForeignKey("IdLocation")] 
        public long LocationId { get; set; }

        [BindNever] 
        [JsonIgnore] 
        public virtual Location? Location { get; set; }
    }
}