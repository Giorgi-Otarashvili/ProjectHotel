using System.Text.Json.Serialization;

namespace Hotel.Models.Dtos
{
    public class ReservationCreateDto
    {
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        [JsonIgnore]
        public DateTime CheckIn => DateTime.Parse(CheckInTime);
        [JsonIgnore]
        public DateTime CheckOut => DateTime.Parse(CheckOutTime);
        public int RoomId { get; set; }
        [JsonIgnore]
        public string? GuestId { get; set; }
    }
}
