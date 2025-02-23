using AutoMapper;
using ConcertApp.Data.DTO;
using ConcertApp.Data.Entity;

namespace ConcertApp.API.Profiles
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
       
            CreateMap<Concert, ConcertDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));


       
            CreateMap<ConcertDto, Concert>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            

   

        }
    }

}
