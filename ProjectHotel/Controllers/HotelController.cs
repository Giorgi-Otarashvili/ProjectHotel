using AutoMapper;
using Hotel.Models.Dtos;
using Hotel.Models.Entities;
using Hotel.Services.Implementations;
using Hotel.Services.Interfases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ProjectHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;
        private readonly IReservationService _reservationService;
        private readonly IMapper _mapper;

        public HotelsController(IHotelService hotelService, IRoomService roomService, IReservationService reservationService, IMapper mapper)
        {
            _hotelService = hotelService;
            _roomService = roomService;
            _reservationService = reservationService;
            _mapper = mapper;
        }
        #region HOTEL ENDPOINTS
        // Create Hotel
        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateDto hotelDto)
        {
            if (hotelDto == null)
                return BadRequest("Invalid hotel data.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("Manager not found");

            hotelDto.ManagerId = userId;

            var createdHotel = await _hotelService.CreateHotelAsync(hotelDto);

            if(createdHotel != null)
                return Ok("Hotel Created");

            return BadRequest();
        }

        // Update Hotel
        [Authorize(Roles = "Manager,Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelCreateDto hotelDTO)
        {
            if (hotelDTO == null)
                return BadRequest("Invalid hotel data.");

            var hotelEntity = _mapper.Map<hotel>(hotelDTO);

            var updatedHotel = await _hotelService.UpdateHotelAsync(id, hotelEntity.Name, hotelEntity.Address, hotelEntity.Rating);
            if (updatedHotel == null)
                return NotFound("Hotel not found.");

            var updatedHotelDTO = _mapper.Map<HotelDTO>(updatedHotel);

            return Ok(updatedHotelDTO);
        }

        // Delete Hotel
        [Authorize(Roles = "Manager,Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var result = await _hotelService.DeleteHotelAsync(id);
            if (!result)
                return NotFound("Hotel not found.");

            return NoContent();
        }

        // Get Hotel by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelService.GetHotelByIdAsync(id);
            if (hotel == null)
                return NotFound("Hotel not found.");

            var hotelDTO = _mapper.Map<HotelDTO>(hotel); 

            return Ok(hotelDTO);
        }

        // Get Hotels by Filter (Country, City, Rating)
        [HttpGet]
        public IActionResult GetHotelsByFilter([FromQuery] string? country, [FromQuery] string? city, [FromQuery] int? rating)
        {
            var hotels = _hotelService.GetHotelsByFilter(country, city, rating);
             var hotelsDTOs = _mapper.Map<IEnumerable<HotelDTO>>(hotels);                            

            return Ok(hotelsDTOs);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("add-manager-to-hotel")]
        public async Task<IActionResult> AddManagerToHotel([FromBody] AddManagerToHotelDto addManagerToHotel)
        {
            var result = await _hotelService.AddManagerToHotelAsync(addManagerToHotel.ManagerId, addManagerToHotel.HotelId);

            if (result)
                return Ok("Manager added to hotel.");
            return BadRequest();
        }
        #endregion

        #region ROOM ENDPOINTS

        // Add Room to Hotel
        [Authorize(Roles = "Manager,Administrator")]
        [HttpPost("{hotelId}/rooms")]
        public async Task<IActionResult> AddRoomToHotel(int hotelId, [FromBody] RoomCreateDto roomDTO)
        {
            if (roomDTO == null || roomDTO.Price <= 0)
                return BadRequest("Invalid room data. Price must be greater than zero.");

            var createdRoom = await _roomService.AddRoomAsync(roomDTO, hotelId);

            if (createdRoom != null)
            {
                return Ok("Created Room");
            }
            return BadRequest();
        }

        // Update Room Details
        [Authorize(Roles = "Manager,Administrator")]
        [HttpPut("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> UpdateRoom(int hotelId, int roomId, [FromBody] RoomCreateDto roomDTO)
        {
            var updatedRoom = await _roomService.UpdateRoomAsync(roomId, roomDTO.Name, roomDTO.Price, roomDTO.IsAvailable);
            if (updatedRoom == null)
                return NotFound("Room not found or does not belong to this hotel.");

            return Ok(_mapper.Map<RoomDTO>(updatedRoom));
        }

        // Delete Room
        [Authorize(Roles = "Manager,Administrator")]
        [HttpDelete("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> DeleteRoom(int hotelId, int roomId)
        {
            var result = await _roomService.DeleteRoomAsync(roomId);
            if (!result)
                return BadRequest("Room cannot be deleted. It may have active reservations.");

            return NoContent();
        }

        // Get Rooms of a Hotel
        [HttpGet("{hotelId}/rooms")]
        public IActionResult GetRoomsByHotel(int hotelId, [FromQuery] bool? isAvailable, [FromQuery] decimal? minPrice, [FromQuery] decimal? maxPrice)
        {
            var rooms = _roomService.GetRoomsByFilter(hotelId, isAvailable, minPrice, maxPrice);
            return Ok(_mapper.ProjectTo<RoomDTO>(rooms));
        }
        #endregion

        #region RESERVATION ENDPOINTS

        [Authorize(Roles ="Guest")]
        [HttpPost("reservations")]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationCreateDto reservationDTO)
        {
            if (reservationDTO == null)
                return BadRequest("Invalid reservation data.");

            //Validations
            if (reservationDTO.CheckIn < DateTime.Now)
                return BadRequest("Check-in date must be in the future.");
            if(reservationDTO.CheckOut <= reservationDTO.CheckIn)
                return BadRequest("Check-out date must be after check-in date.");

            var room = await _roomService.GetRoomByIdAsync(reservationDTO.RoomId);

            if(!room.IsAvailable)
                return BadRequest("Room is not available");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("Manager not found");

            reservationDTO.GuestId = userId;

            var createdReservation = await _reservationService.CreateReservationAsync(reservationDTO);

            if (createdReservation != null)
                return Ok("Reservation Created");
            return BadRequest();
        }

        [Authorize(Roles = "Guest,Administrator")]
        [HttpPut("reservations/{reservationId}")]
        public async Task<IActionResult> UpdateReservation(int reservationId, [FromBody] ReservationUpdateDto reservationDTO)
        {
            var updateReservation = await _reservationService.UpdateReservationAsync(reservationId, reservationDTO.CheckIn, reservationDTO.CheckOut);

            if (updateReservation){
                return Ok("Reservation Updated");
            }
            return BadRequest();
        }

        [Authorize(Roles = "Guest,Administrator")]
        [HttpDelete("reservations/{reservationId}")]
        public async Task<IActionResult> DeleteReservation(int reservationId)
        {
            var result = await _reservationService.DeleteReservationAsync(reservationId);

            if (result)
                return Ok("Reservation deleted");
            return BadRequest();
        }

        #endregion

    }
}
