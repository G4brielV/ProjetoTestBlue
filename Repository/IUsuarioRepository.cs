using System.Threading.Tasks;
using ProjetoTestBlue.Models;

namespace ProjetoTestBlue.Repository
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<Usuario> AddUsuarioAsync(Usuario usuario);
        Task<bool> EmailExistsAsync(string email);
        Task<Usuario> FindByIdAsync(int id);
        Task<Usuario> UpdateUsuarioAsync(Usuario usuarioNovo);
        Task<bool> DeleteUsuarioAsync(Usuario usuario);


    }
}