using AutoMapper;
using ConcertApp.Data.DTO;
using ConcertApp.Data.Entity;

namespace ConcertApp.API.Profiles
{
    public class PerformanceProfile : Profile
    {
        public PerformanceProfile()
        {
            //Map Entity to DTO
            // Note that "dest.Done" gets its value from "src.Completed"
            // Note that there is no "dest.Comments" to match "src.Comments"
            CreateMap<Performance, PerformanceDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.DateTime))
            .ForMember(dest => dest.ConcertId, opt => opt.MapFrom(src => src.ConcertId));


            //Map DTO to Entity
            //Note that "dest.Completed" gets its value from "src.Done"
            //Note that "dest.Comments" has its value set to string.Empty
            CreateMap<PerformanceDto, Performance>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.DateTime, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.ConcertId, opt => opt.MapFrom(src => src.ConcertId));


            //CreateMap<Performance, PerformanceDto>().ReverseMap();

        }
    }
}
