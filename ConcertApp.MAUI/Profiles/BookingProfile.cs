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
            // Map Model to DTO
            // Note that "dest.Name" gets its value from "src.TaskName"
            //CreateMap<Booking, BookingDto>()
            //    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.TaskName))
            //    .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
            //    .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Done));
            // Map DTO to Model
            // Note that "dest.TaskName" gets its value from "src.Name"
            //CreateMap<TodoItemDto, TodoItem>()
            //    .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
            //    .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
            //    .ForMember(dest => dest.Done, opt => opt.MapFrom(src => src.Done));
            //CreateMap<TodoItem, TodoItemDto>().ReverseMap();
        }
    }
}
