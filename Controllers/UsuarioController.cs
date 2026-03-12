using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoTestBlue.Data;
using ProjetoTestBlue.Models;

namespace ProjetoTestBlue.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public UsuarioController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _appDbContext.ProjTestBlue.ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _appDbContext.ProjTestBlue.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuario não encontrado");
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> AddUsuario([FromBody]Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _appDbContext.ProjTestBlue.Add(usuario);
            await _appDbContext.SaveChangesAsync();

            return Created("Usuário adicionado", usuario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario usuarioAtualizado)
        {
            var usuarioAtual = await _appDbContext.ProjTestBlue.FindAsync(id);
            if (usuarioAtual == null)
            {
                return NotFound("Personagem não encontrado");
            }

            _appDbContext.Entry(usuarioAtual).CurrentValues.SetValues(usuarioAtualizado);
            await _appDbContext.SaveChangesAsync();

            return StatusCode(201, usuarioAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _appDbContext.ProjTestBlue.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Personagem não encontrado");
            }

            _appDbContext.ProjTestBlue.Remove(usuario);
            await _appDbContext.SaveChangesAsync();

            return Ok("Usuario deletado");
        }


    }
}