using ProjetoTestBlue.Models;

namespace ProjetoTestBlue.Repository
{
    public interface ITodoListRepository
    {
        Task<IEnumerable<TodoList>> GetTodoListsAsync(int userId);
        Task<TodoList?> FindByIdAsync(int id);
        Task CriarTodoListAsync(TodoList todoList);
        Task<TodoList> UpdateTodoListAsync(TodoList todoListNovo);
        Task<bool> DeleteTodoListAsync(TodoList todoList);

    }
}