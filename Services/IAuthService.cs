using Microsoft.AspNetCore.Identity.Data;
using ProjetoTestBlue.DTOs;

namespace ProjetoTestBlue.Services.Impl
{
    public interface IAuthService
    {
        Task<Result<CadastroResponse>> CadastroAsync(CadastroRequest request);
        Task<Result<LoginResponse>> LoginAsync(DTOs.LoginRequest request);
    }
}