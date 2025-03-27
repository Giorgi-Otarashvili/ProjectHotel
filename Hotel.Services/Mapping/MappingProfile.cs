using AutoMapper;
using Hotel.Models.Dtos;
using Hotel.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Hotel <-> HotelDTO
            CreateMap<hotel, HotelDTO>()
                .ForMember(dest => dest.Manager, opt => opt.MapFrom(src => src.Manager))
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms))
                .ReverseMap();

            // Manager <-> ManagerDTO
            CreateMap<ApplicationUser, ManagerDTO>().ReverseMap();

            // Room <-> RoomDTO
            CreateMap<Room, RoomDTO>().ReverseMap();
        }
    }
}
 