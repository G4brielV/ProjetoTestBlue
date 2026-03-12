using AutoMapper;
using ProjetoTestBlue.DTOs;
using ProjetoTestBlue.Models;

namespace ProjetoTestBlue.Mappings
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioRequest, Usuario>();
            CreateMap<UpdateUsuarioRequest, Usuario>();
            CreateMap<Usuario, UsuarioResponse>();
        }
    }
}