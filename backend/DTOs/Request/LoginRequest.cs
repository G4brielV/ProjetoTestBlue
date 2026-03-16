using System.ComponentModel.DataAnnotations;

namespace ProjetoTestBlue.DTOs
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "Senha deve ter pelo menos 6 caracteres")]
        public string Senha { get; set; }
    }
}