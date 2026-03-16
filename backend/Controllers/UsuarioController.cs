using Microsoft.AspNetCore.Mvc;
using ProjetoTestBlue.DTOs;
using ProjetoTestBlue.Services;
using Microsoft.AspNetCore.Authorization;

namespace ProjetoTestBlue.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetUsuarios()
        {   
            // TODO: Paginação
            var usuarios = await _usuarioService.GetUsuariosAsync();
            return Ok(usuarios.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponse>> GetUsuarioById(int id)
        {
            var result = await _usuarioService.GetByIdAsync(id);
            if (!result.IsSuccess) return NotFound(new { message = result.Error });
            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UpdateUsuarioRequest request)
        {
            Result<UsuarioResponse> result = await _usuarioService.UpdateUsuarioAsync(id, request);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var result = await _usuarioService.DeleteUsuarioAsync(id);
            if (!result.IsSuccess)
                return NotFound(result.Error);
            return NoContent();
        }

    }
}