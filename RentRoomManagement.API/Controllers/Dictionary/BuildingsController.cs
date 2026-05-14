using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentRoomManagement.BL.Tenant.Dictonary.BuildingBL;
using RentRoomManagement.Common.Entities.Dictionary;
using RentRoomManagement.Common.Entitites.DTO;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    [Authorize]
    public class BuildingsController : BasesController<BuildingEntity, BuildingDto>
    {
        private readonly IBuildingBL _buildingBL;

        public BuildingsController(IBuildingBL businessLayer) : base(businessLayer)
        {
            _buildingBL = businessLayer;
        }

        /// <summary>
        /// Đặt tòa nhà chỉ định là đang sử dụng, các tòa nhà còn lại của cùng admin sẽ chuyển sang không sử dụng
        /// </summary>
        [HttpPatch("{buildingId}/set-active")]
        public async Task<IActionResult> SetActiveBuilding([FromRoute] Guid buildingId)
        {
            var uidClaim = User.FindFirst("uid")?.Value;
            if (!Guid.TryParse(uidClaim, out var userId))
                return Unauthorized();

            var success = await _buildingBL.SetActiveBuilding(buildingId, userId);
            if (!success)
                return BadRequest();

            return NoContent();
        }
    }
}
