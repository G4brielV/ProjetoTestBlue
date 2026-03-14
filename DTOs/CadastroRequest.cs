using System.ComponentModel.DataAnnotations;

namespace ProjetoTestBlue.DTOs
{
    public class CadastroRequest
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [MaxLength(50, ErrorMessage = "Nome não pode exceder 50 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "Senha deve ter pelo menos 6 caracteres")]
        public string Senha { get; set; }
    }
}