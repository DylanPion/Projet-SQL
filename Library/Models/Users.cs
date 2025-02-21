using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Users
    {
        public Users(string name, string email, string role)
        {
            Name = name;
            Email = email;
            Role = role;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(Administrateur|Lecteur)$", ErrorMessage = "Role must be either 'Administrateur' or 'Lecteur'.")]
        public string Role { get; set; }
    }
}