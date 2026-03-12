using System.Threading.Tasks;
using ProjetoTestBlue.DTOs;

namespace ProjetoTestBlue.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioResponse> AddUsuarioAsync(CreateUsuarioRequest request);
        Task<UsuarioResponse> UpdateUsuarioAsync(int id, UpdateUsuarioRequest request);
        Task<bool> DeleteUsuarioAsync(int id);
        Task<UsuarioResponse> FindByIdAsync(int id);
        Task<IEnumerable<UsuarioResponse>> GetUsuariosAsync();
    }
}