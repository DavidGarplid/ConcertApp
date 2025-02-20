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
    public class PerformanceProfile : Profile
    {
        public PerformanceProfile()
        {
            CreateMap<Performance, PerformanceDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.ConcertId, opt => opt.MapFrom(src => src.ConcertId));


            CreateMap<PerformanceDto, Performance>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.ConcertId, opt => opt.MapFrom(src => src.ConcertId));
        }
    }
}
