using AutoMapper;
using ProjetoTestBlue.DTOs.Request;
using ProjetoTestBlue.DTOs.Response;
using ProjetoTestBlue.Models;

namespace ProjetoTestBlue.Mappings
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<CardRequest, Card>()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Description));
            CreateMap<Card, CardResponse>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}