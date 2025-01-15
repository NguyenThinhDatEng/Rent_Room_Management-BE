using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentRoomManagement.Common.Entitites.TDto;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Param;
using RentRoomManagement.Common.Resources;
using RentRoomManagement.DL.RoomManagement.RenterDL;

namespace RentRoomManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class RentersController : ControllerBase
    {
        #region Field

        private IRenterDL _renterDL;

        #endregion

        #region Constructor

        public RentersController(IRenterDL renterDL)
        {
            _renterDL = renterDL;
        }
        #endregion


        [HttpPost("linking")]
        public async Task<IActionResult> LinkToBuilding([FromBody] BuldingLinkingParam param)
        {
            try
            {
                // Gọi đến Business Layer
                var result = await _renterDL.LinkToBuilding(param);
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = (int)ErrorCode.Exception,
                    DevMsg = Errors.DevMsg_Exception,
                    UserMsg = Errors.UserMsg_Exception,
                    MoreInfo = new List<string> { ex.Message },
                });
            }
        }
    }
}
