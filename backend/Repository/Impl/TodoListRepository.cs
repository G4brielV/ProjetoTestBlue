using Microsoft.EntityFrameworkCore;
using ProjetoTestBlue.Data;
using ProjetoTestBlue.Models;


namespace ProjetoTestBlue.Repository.Impl
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly AppDbContext _context;

        public TodoListRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CriarTodoListAsync(TodoList todoList)
        {
            _context.TodoLists.Add(todoList);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteTodoListAsync(TodoList todoList)
        {
            _context.TodoLists.Remove(todoList);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TodoList?> FindByIdAsync(int id)
        {
            return await _context.TodoLists.Include(t => t.Cards).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TodoList>> GetTodoListsAsync(int userId)
        {
            return await _context.TodoLists
                .Where(t => t.UsuarioId == userId)
                .Include(t => t.Cards)
                .ToListAsync();
        }

        public async Task<TodoList> UpdateTodoListAsync(TodoList todoListNovo)
        {
            _context.TodoLists.Update(todoListNovo);
            await _context.SaveChangesAsync();
            return todoListNovo;
        }
    }
}