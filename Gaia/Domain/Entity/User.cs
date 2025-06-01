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
    
    //Strings
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string cpf { get; set; }
    
    //Data
    public DateTime creationDate { get; set; }
    
    //Boolean
    public bool isActive { get; set; }
    
    //Enums
    public TypeUsers typeUser { get; set; }
    
    //Relacionemto
    public ICollection<Requestion> requestions { get; set; } = new List<Requestion>();


    public User() {}

    public bool validCpf()
    {
        return Regex.IsMatch(this.cpf, @"^\d{11}$");
    }

    public bool validEmail()
    {
        return Regex.IsMatch(this.email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
    }

    public bool ValidPassword()
    {
        return Regex.IsMatch(this.password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");
    }

}