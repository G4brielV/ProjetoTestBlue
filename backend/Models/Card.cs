namespace ProjetoTestBlue.Models
{
    public class Card
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string? Descricao { get; set; }
    public bool IsCompleted { get; set; }
    public int Posicao { get; set; } 
    public int ListId { get; set; }
    public virtual TodoList TodoList { get; set; }
}
}