using AutoMapper;
using ProjetoTestBlue.DTOs;
using ProjetoTestBlue.Models;

namespace ProjetoTestBlue.Mappings
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CadastroRequest, Usuario>();
            CreateMap<UpdateUsuarioRequest, Usuario>();

            CreateMap<Usuario, UsuarioResponse>();
            CreateMap<Usuario, CadastroResponse>();
        }
    }
}