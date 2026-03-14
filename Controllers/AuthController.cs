using Microsoft.AspNetCore.Mvc;
using ProjetoTestBlue.Services;
using ProjetoTestBlue.Services.Impl;
using ProjetoTestBlue.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace ProjetoTestBlue.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("cadastro")]
        public async Task<ActionResult<CadastroResponse>> Cadastro([FromBody] CadastroRequest request)
        {
            Result<CadastroResponse> result = await _authService.CadastroAsync(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(Cadastro), new { id = result.Data.Id }, result.Data);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            Result<LoginResponse> result = await _authService.LoginAsync(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Data);
        }
    }
}