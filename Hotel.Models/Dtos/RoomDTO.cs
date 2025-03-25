﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Dtos
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int HotelId { get; set; }
    }
}
