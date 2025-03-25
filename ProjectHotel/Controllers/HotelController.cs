using AutoMapper;
using Hotel.Models.Dtos;
using Hotel.Models.Entities;
using Hotel.Services.Interfases;
using Microsoft.AspNetCore.Mvc;

namespace ProjectHotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly IMapper _mapper;

        public HotelController(IHotelService hotelService, IMapper mapper)
        {
            _hotelService = hotelService;
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
    }
}
