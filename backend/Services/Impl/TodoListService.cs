using AutoMapper;
using ProjetoTestBlue.DTOs;
using ProjetoTestBlue.DTOs.Request;
using ProjetoTestBlue.DTOs.Response;
using ProjetoTestBlue.Models;
using ProjetoTestBlue.Repository;
using ProjetoTestBlue.Services;

namespace ProjetoTestBlue.Services.Impl
{
    public class TodoListService : ITodoListService
    {
        private readonly ITodoListRepository _repository;
        private readonly IMapper _mapper;

        public TodoListService(ITodoListRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<TodoListResponse>> CriarTodoListAsync(int id, TodoListRequest request)
        {
            var todoList = _mapper.Map<TodoList>(request);
            todoList.UsuarioId = id;
            await _repository.CriarTodoListAsync(todoList);

            Console.WriteLine("CHEGOUUUUUUUUUUUUUUUUUUU");

            var response = _mapper.Map<TodoListResponse>(todoList);
            return Result<TodoListResponse>.Success(response);
 
        }

        public async Task<Result<bool>> DeleteTodoListAsync(int id, int userId)
        {
            var todoList = await _repository.FindByIdAsync(id);
            if (todoList == null || todoList.UsuarioId != userId)
            {
                return Result<bool>.Failure("TodoList not found or access denied");
            }
            await _repository.DeleteTodoListAsync(todoList);
            return Result<bool>.Success(true);
        }

        public async Task<Result<TodoListResponse>> FindByIdAsync(int id, int userId)
        {
            var todoList = await _repository.FindByIdAsync(id);
            if (todoList == null || todoList.UsuarioId != userId)
            {
                return Result<TodoListResponse>.Failure("TodoList not found or access denied");
            }
            var response = _mapper.Map<TodoListResponse>(todoList);
            return Result<TodoListResponse>.Success(response);
        }

        public async Task<Result<IEnumerable<TodoListResponse>>> GetTodoListsAsync(int userId)
        {
            var todoLists = await _repository.GetTodoListsAsync(userId);
            var response = _mapper.Map<IEnumerable<TodoListResponse>>(todoLists);
            return Result<IEnumerable<TodoListResponse>>.Success(response);
        }

        public async Task<Result<TodoListResponse>> UpdateTodoListAsync(int id, TodoListRequest request, int userId)
        {
            var todoList = await _repository.FindByIdAsync(id);
            if (todoList == null || todoList.UsuarioId != userId)
            {
                return Result<TodoListResponse>.Failure("TodoList not found or access denied");
            }
            todoList.Titulo = request.Titulo;
            await _repository.UpdateTodoListAsync(todoList);
            var response = _mapper.Map<TodoListResponse>(todoList);
            return Result<TodoListResponse>.Success(response);
        }
    }
}