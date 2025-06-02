using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Gaia.Domain.Enum;

namespace Gaia.Domain.Entity;

[Table("Users")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long idUser { get; set; }

    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string cpf { get; set; }

    public DateTime creationDate { get; set; }

    public bool active { get; set; }

    public TypeUsers typeUser { get; set; }

    public ICollection<Requestion> requestions { get; set; } = new List<Requestion>();

    public bool ValidCpf() => Regex.IsMatch(cpf, @"^\d{11}$");

    public bool ValidEmail() => Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

    public bool ValidPassword() =>
        Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");
}
