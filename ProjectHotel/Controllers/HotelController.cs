using AutoMapper;
using Hotel.Models.Dtos;
using Hotel.Models.Entities;
using Hotel.Services.Implementations;
using Hotel.Services.Interfases;
using Microsoft.AspNetCore.Mvc;

namespace ProjectHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly IRoomService _roomService;
        private readonly IManagerService _managerService;
        private readonly IGuestService _guestService;
        private readonly IMapper _mapper;

        public HotelController(IHotelService hotelService, IRoomService roomService, IManagerService managerService, IGuestService guestService, IMapper mapper)
        {
            _hotelService = hotelService;
            _roomService = roomService;
            _managerService = managerService;
            _guestService = guestService;
            _mapper = mapper;
        }
        // Create Hotel
        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelDTO hotelDTO)
        {
            if (hotelDTO == null)
                return BadRequest("Invalid hotel data.");
            var hotelEntity = _mapper.Map<hotel>(hotelDTO);

            var createdHotel = await _hotelService.CreateHotelAsync(hotelEntity);

            var createdHotelDTO = _mapper.Map<HotelDTO>(createdHotel);


            return CreatedAtAction(nameof(GetHotelById), new { id = createdHotelDTO.Id }, createdHotelDTO);
        }

        // Update Hotel
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelDTO hotelDTO)
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

        // ---------------------- Room Endpoints ----------------------

        // Add Room to Hotel
        [HttpPost("{hotelId}/rooms")]
        public async Task<IActionResult> AddRoomToHotel(int hotelId, [FromBody] RoomDTO roomDTO)
        {
            if (roomDTO == null || roomDTO.Price <= 0)
                return BadRequest("Invalid room data. Price must be greater than zero.");

            var roomEntity = _mapper.Map<Room>(roomDTO);
            roomEntity.HotelId = hotelId;

            var createdRoom = await _roomService.AddRoomAsync(roomEntity);
            return Ok(_mapper.Map<RoomDTO>(createdRoom));
        }

        // Update Room Details
        [HttpPut("{hotelId}/rooms/{roomId}")]
        public async Task<IActionResult> UpdateRoom(int hotelId, int roomId, [FromBody] RoomDTO roomDTO)
        {
            var updatedRoom = await _roomService.UpdateRoomAsync(roomId, roomDTO.Name, roomDTO.Price, roomDTO.IsAvailable);
            if (updatedRoom == null)
                return NotFound("Room not found or does not belong to this hotel.");

            return Ok(_mapper.Map<RoomDTO>(updatedRoom));
        }

        // Delete Room
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
        // ---------------------- Guest Endpoints ----------------------

        // Register Guest
        [HttpPost("guests/register")]
        public async Task<IActionResult> RegisterGuest([FromBody] GuestDTO guestDTO)
        {
            if (guestDTO == null)
                return BadRequest("Invalid guest data.");

            var result = await _guestService.RegisterGuestAsync(guestDTO);

            if (result != "Guest registered successfully!")
                return BadRequest(result); 

            return Ok(result);
        }

        // Update Guest
        [HttpPut("guests/{id}")]
        public async Task<IActionResult> UpdateGuest(int id, [FromBody] GuestDTO guestDTO)
        {
            var updatedGuest = await _guestService.UpdateGuestAsync(id, guestDTO);
            if (updatedGuest == null)
                return NotFound("Guest not found.");

            return Ok(updatedGuest);
        }

        // Delete Guest (if no active reservations)
        [HttpDelete("guests/{id}")]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            var result = await _guestService.DeleteGuestAsync(id); 

            if (result == "Guest has active reservations and cannot be deleted.")
                return BadRequest(result);  

            if (result == "Guest not found.")
                return NotFound(result);

            return Ok("Guest successfully deleted.");
        }

        // ---------------------- Manager Endpoints ----------------------

        // Register Manager
        [HttpPost("managers/register")]
        public async Task<IActionResult> RegisterManager([FromBody] ManagerDTO managerDTO)
        {
            var manager = await _managerService.CreateManagerAsync(managerDTO);
            return CreatedAtAction(nameof(GetManagerById), new { managerId = manager.Id }, manager);
        }

        // Update Manager
        [HttpPut("managers/{id}")]
        public async Task<IActionResult> UpdateManager(int id, [FromBody] ManagerDTO managerDTO)
        {
            var updatedManager = await _managerService.UpdateManagerAsync(id, managerDTO);
            if (updatedManager == null) return NotFound("Manager not found.");
            return Ok(updatedManager);
        }

        // Delete Manager
        [HttpDelete("managers/{id}")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            var success = await _managerService.DeleteManagerAsync(id);
            if (!success) return NotFound("Manager not found.");
            return NoContent();
        }

        // Get Manager by ID
        [HttpGet("managers/{id}")]
        public async Task<IActionResult> GetManagerById(int id)
        {
            var manager = await _managerService.GetManagerByIdAsync(id);
            if (manager == null) return NotFound("Manager not found.");
            return Ok(manager);
        }

        // Get All Managers
        [HttpGet("managers")]
        public async Task<ActionResult<IEnumerable<ManagerDTO>>> GetAllManagers()
        {
            var managers = await _managerService.GetAllManagersAsync();
            return Ok(managers);
        }
    }
}
