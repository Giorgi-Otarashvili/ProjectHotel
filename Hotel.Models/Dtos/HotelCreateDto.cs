using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hotel.Models.Dtos
{
    public class HotelCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        [JsonIgnore]
        public string ManagerId { get; set; } = string.Empty;
        public List<RoomCreateDto> Rooms { get; set; } = new();
    }
}
