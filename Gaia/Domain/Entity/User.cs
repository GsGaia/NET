using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Gaia.Domain.Enum;

namespace Gaia.Domain.Entity
{

    [Table("USERS")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdUser { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cpf { get; set; }

        public DateTime CreationDate { get; set; }

        public TypeUsers TypeUser { get; set; }
        
        public ICollection<Requestion> Requestions { get; set; } = new List<Requestion>();

        public bool ValidCpf() => Regex.IsMatch(Cpf, @"^\d{11}$");

        public bool ValidEmail() => Regex.IsMatch(Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

        public bool ValidPassword() =>
            Regex.IsMatch(Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");
    }
}
