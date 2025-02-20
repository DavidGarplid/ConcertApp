using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ConcertApp.Data.DTO;
using ConcertApp.MAUI.Models;

namespace ConcertApp.MAUI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));



            CreateMap<UserDto, User>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        }
    }
}
