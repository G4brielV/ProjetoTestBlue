using AutoMapper;
using ProjetoTestBlue.DTOs.Request;
using ProjetoTestBlue.DTOs.Response;
using ProjetoTestBlue.Models;

namespace ProjetoTestBlue.Mappings
{
    public class TodoListProfile : Profile
    {
        public TodoListProfile()
        {
            CreateMap<TodoListRequest, TodoList>();
            CreateMap<TodoList, TodoListResponse>();
        }
    }
}