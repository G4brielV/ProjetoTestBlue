namespace ProjetoTestBlue.Models
{
    public class TodoList
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public int UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; } 
    public virtual ICollection<Card> Cards { get; set; }
}
}