using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoTestBlue.DTOs.Request;
using ProjetoTestBlue.DTOs.Response;
using ProjetoTestBlue.Services;
using ProjetoTestBlue.Services.Impl;

namespace ProjetoTestBlue.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListService _todoListService;

        public TodoListController(ITodoListService todoListService)
        {
            _todoListService = todoListService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoListResponse>>> GetTodoLists()
        {   
            // TODO: Paginação
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            int userId = int.Parse(userIdClaim.Value);
            var lists = await _todoListService.GetTodoListsAsync(userId);
            return Ok(lists.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoListResponse>> GetTodoList(int id)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            int userId = int.Parse(userIdClaim.Value);
            var result = await _todoListService.FindByIdAsync(id, userId);
            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<ActionResult<TodoListResponse>> AddTodoList(TodoListRequest todoListRequest)
        {   
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            int userId = int.Parse(userIdClaim.Value);
            var result = await _todoListService.CriarTodoListAsync(userId, todoListRequest);
            return CreatedAtAction(nameof(GetTodoList), new { id = result.Data.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoList(int id, TodoListRequest request)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            int userId = int.Parse(userIdClaim.Value);
            var result = await _todoListService.UpdateTodoListAsync(id, request, userId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoList(int id)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            int userId = int.Parse(userIdClaim.Value);
            var result = await _todoListService.DeleteTodoListAsync(id, userId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        }
    }
}