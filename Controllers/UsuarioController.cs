using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ProjetoTestBlue.DTOs;
using ProjetoTestBlue.Services;

namespace ProjetoTestBlue.Controllers
{
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
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponse>> GetUsuario(int id)
        {
            var result = await _usuarioService.FindByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUsuario([FromBody] CreateUsuarioRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _usuarioService.AddUsuarioAsync(request);
                return CreatedAtAction(nameof(GetUsuario), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                // business rule violation (e.g. email already taken)
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UpdateUsuarioRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _usuarioService.UpdateUsuarioAsync(id, request);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                // business rule violation (e.g. email already taken)
                return BadRequest(new { message = ex.Message });
            }


        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var deleted = await _usuarioService.DeleteUsuarioAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

    }
}