using ProjetoTestBlue.DTOs;
using ProjetoTestBlue.Models;

namespace ProjetoTestBlue.Services
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
    }
}