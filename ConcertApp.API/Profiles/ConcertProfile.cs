using AutoMapper;
using ConcertApp.Data.DTO;
using ConcertApp.Data.Entity;

namespace ConcertApp.API.Profiles
{
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            // Map Entity to DTO
            // Note that "dest.Done" gets its value from "src.Completed"
            // Note that there is no "dest.Comments" to match "src.Comments"
            CreateMap<Concert, ConcertDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
            .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Completed));
            // Map DTO to Entity
            // Note that "dest.Completed" gets its value from "src.Done"
            // Note that "dest.Comments" has its value set to string.Empty
            //CreateMap<ConcertDto, Concert>()
            //.ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //.ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
            //.ForMember(dest => dest.Completed, opt => opt.MapFrom(src => src.Done))
            //.ForMember(dest => dest.Comments, opt => opt.MapFrom(src => string.Empty));
            //CreateMap<TodoItem, TodoItemDto>().ReverseMap();

        }
    }

}
