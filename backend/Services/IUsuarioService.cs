using System.Threading.Tasks;
using ProjetoTestBlue.DTOs;

namespace ProjetoTestBlue.Services
{
    public interface IUsuarioService
    {
        Task<Result<UsuarioResponse>> UpdateUsuarioAsync(int id, UpdateUsuarioRequest request);
        Task<Result<UsuarioResponse>>  GetByIdAsync(int id);
        Task<Result<bool>> DeleteUsuarioAsync(int id);
        Task<Result<IEnumerable<UsuarioResponse>>> GetUsuariosAsync();
    }
}