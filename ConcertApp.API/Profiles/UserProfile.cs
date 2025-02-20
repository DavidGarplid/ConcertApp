using AutoMapper;
using ConcertApp.Data.DTO;
using ConcertApp.Data.Entity;

namespace ConcertApp.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //Map Entity to DTO
            // Note that "dest.Done" gets its value from "src.Completed"
            // Note that there is no "dest.Comments" to match "src.Comments"
            CreateMap<User, UserDto>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.email))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.password));



            //Map DTO to Entity
            //Note that "dest.Completed" gets its value from "src.Done"
            //Note that "dest.Comments" has its value set to string.Empty
            CreateMap<UserDto, User>()
            .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.password, opt => opt.MapFrom(src => src.Password));
            


            //CreateMap<User, UserDto>().ReverseMap();

        }
    }
}
