using Microsoft.AspNetCore.Mvc;
using RentRoomManagement.BL.Location;

namespace RentRoomManagement.API.Controllers.Location
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly LocationBL _locationBL = new LocationBL();

        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var result = await _locationBL.GetAllLocations();
            return Ok(result);
        }
    }
}
