using System.ComponentModel.DataAnnotations;

namespace WebDatingApp.API.Domain.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Nome de usuário é um campo obrigatório")]
        public string Username { get; set; }
        
        [Required(ErrorMessage ="Senha é um campo obrigatório")]
        [StringLength(12, MinimumLength = 4, ErrorMessage  = "A senha deve conter de 4 à 12 caracteres")]
        public string Password { get; set; }
    }
}