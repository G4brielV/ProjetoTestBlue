using ProjetoTestBlue.DTOs;
using ProjetoTestBlue.DTOs.Request;
using ProjetoTestBlue.DTOs.Response;

namespace ProjetoTestBlue.Services
{
    public interface ITodoListService
    {   
        Task<Result<IEnumerable<TodoListResponse>>> GetTodoListsAsync(int userId);
        Task<Result<TodoListResponse>> FindByIdAsync(int id, int userId);
        Task<Result<TodoListResponse>> CriarTodoListAsync(int id, TodoListRequest request);
        Task<Result<TodoListResponse>> UpdateTodoListAsync(int id, TodoListRequest request, int userId);
        Task<Result<bool>> DeleteTodoListAsync(int id, int userId);
        
    }
}