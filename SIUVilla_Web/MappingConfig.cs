using AutoMapper;
using SIUVilla_Web.Models.DTO;


namespace SIUVilla_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
           CreateMap<VillaDTO,VillaCreateDTO>().ReverseMap();
           CreateMap<VillaDTO,VillaUpdateDTO>().ReverseMap();
           CreateMap<VillaNumberDTO,VillaNumberCreateDTO>().ReverseMap();
           CreateMap<VillaNumberDTO,VillaNumberUpdateDTO>().ReverseMap();
        }
    }
}
