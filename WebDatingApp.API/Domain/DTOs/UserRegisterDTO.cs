using System.ComponentModel.DataAnnotations;
using System;

namespace WebDatingApp.API.Domain.DTOs
{
    public class UserRegisterDTO
    {
        // Validações
        [Required(ErrorMessage = "Nome de usuário é um campo obrigatório")]
        public string Username { get; set; }
        
        [Required(ErrorMessage ="Senha é um campo obrigatório")]
        [StringLength(12, MinimumLength = 4, ErrorMessage  = "A senha deve conter de 4 à 12 caracteres")]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string KnownAs { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public UserRegisterDTO ()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}
