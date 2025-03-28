﻿using Hotel.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Dtos
{
    public class HotelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public string ManagerId { get; set; } = string.Empty;
        public List<RoomDTO> Rooms { get; set; } = new();
    }

}
