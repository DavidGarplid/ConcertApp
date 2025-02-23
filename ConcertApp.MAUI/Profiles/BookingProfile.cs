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
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            
            CreateMap<Booking, Booking>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.PerformanceID, opt => opt.MapFrom(src => src.PerformanceID));
            
            CreateMap<BookingDto, Booking>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.PerformanceID, opt => opt.MapFrom(src => src.PerformanceID));
            
        }
    }
}
