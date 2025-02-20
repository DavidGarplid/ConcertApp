using AutoMapper;
using ConcertApp.Data.DTO;
using ConcertApp.Data.Entity;

namespace ConcertApp.API.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            //Map Entity to DTO
            // Note that "dest.Done" gets its value from "src.Completed"
            // Note that there is no "dest.Comments" to match "src.Comments"
            CreateMap<Booking, BookingDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.PerformanceID, opt => opt.MapFrom(src => src.PerformanceID));


            //Map DTO to Entity
            //Note that "dest.Completed" gets its value from "src.Done"
            //Note that "dest.Comments" has its value set to string.Empty
            CreateMap<BookingDto, Booking>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserID))
            .ForMember(dest => dest.PerformanceID, opt => opt.MapFrom(src => src.PerformanceID));
            

            //CreateMap<Booking, BookingDto>().ReverseMap();

        }
    }
}
