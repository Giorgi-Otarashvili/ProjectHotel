using Hotel.Models.Dtos;
using Hotel.Services.Interfases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ProjectHotel.Controllers
{
    [Route("api/managers")]
    [ApiController]
    //[Authorize(Roles = "Admin")] 
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        //[HttpPost("register")]
        //public async Task<IActionResult> RegisterManager([FromBody] ManagerDTO managerDTO)
        //{
        //    var manager = await _managerService.CreateManagerAsync(managerDTO);
        //    return CreatedAtAction(nameof(GetManagerById), new { managerId = manager.Id }, manager);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateManager(int id, [FromBody] ManagerDTO managerDTO)
        //{
        //    var updatedManager = await _managerService.UpdateManagerAsync(id, managerDTO);
        //    if (updatedManager == null) return NotFound();
        //    return Ok(updatedManager);
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteManager(int id)
        //{
        //    var success = await _managerService.DeleteManagerAsync(id);
        //    if (!success) return NotFound();
        //    return NoContent();
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetManagerById(int id)
        //{
        //    var manager = await _managerService.GetManagerByIdAsync(id);
        //    if (manager == null) return NotFound();
        //    return Ok(manager);
        //}

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ManagerDTO>>> GetAllManagers()
        //{
        //    var managers = await _managerService.GetAllManagersAsync();
        //    return Ok(managers);
        //}
    }
}

