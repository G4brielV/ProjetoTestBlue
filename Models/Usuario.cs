using System.ComponentModel.DataAnnotations;

namespace ProjetoTestBlue.Models
{
    public class Usuario
    {
        [Key]        
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome não pode ser nulo")]
        [MaxLength(50, ErrorMessage = "Nome não pode ser maior que 50 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Email não pode ser nulo")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha não pode ser nulo")]
        public string Senha { get; set; }
    }
}