namespace ProjetoTestBlue.DTOs.Response
{
    public class TodoListResponse
    {
    public int Id { get; set; }
    public string Titulo { get; set; }
    public List<CardResponse> Cards { get; set; } = new();
    }
}