using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hotel.Models.Dtos
{
    public class ReservationUpdateDto
    {
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        [JsonIgnore]
        public DateTime CheckIn => DateTime.Parse(CheckInTime);
        [JsonIgnore]
        public DateTime CheckOut => DateTime.Parse(CheckOutTime);
    }
}
