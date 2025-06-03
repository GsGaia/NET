using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Gaia.Domain.Entity
{

    [Table("REQUESTION")]
    public class Requestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdRequestion { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public DateTime RequestDate { get; set; }


        [BindNever] 
        [JsonIgnore] 
        public virtual User? User { get; set; }
        
        [BindNever] 
        [JsonIgnore] 
        public virtual Location? Location { get; set; }

        [JsonPropertyName("idUser")] 
        public long IdUser { get; set; }

        [JsonPropertyName("idLocation")] 
        public long IdLocation { get; set; }
    }
}