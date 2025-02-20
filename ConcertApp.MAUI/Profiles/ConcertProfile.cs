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
    public class ConcertProfile : Profile
    {
        public ConcertProfile()
        {
            // Map Model to DTO
            CreateMap<Concert, ConcertDto>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));


            // Map DTO to Model
            CreateMap<ConcertDto, Concert>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
