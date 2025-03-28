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

            CreateMap<RoomCreateDto, Room>();

            CreateMap<hotel, HotelDTO>().ReverseMap();

            // Hotel <-> HotelDTO
            CreateMap<hotel, HotelCreateDto>()
                .ForMember(dest => dest.Rooms, opt => opt.Ignore()).ReverseMap();

            // Manager <-> ManagerDTO
            CreateMap<ApplicationUser, ManagerDTO>().ReverseMap();

            // Room <-> RoomDTO
            CreateMap<Room, RoomDTO>().ReverseMap();
        }
    }
}
 