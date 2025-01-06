using Microsoft.AspNetCore.Mvc;
using RentRoomManagement.BL;
using RentRoomManagement.BL.RoomManagement;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Entitites.RoomManangement;
using RentRoomManagement.Common.Entitites.TDto;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Query;
using RentRoomManagement.Common.Resources;

namespace RentRoomManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HouseholdsController : ControllerBase
    {
        #region Field

        private IBaseBL<HouseholdEntity, HouseholdDto> _baseBL;

        #endregion

        #region Constructor

        public HouseholdsController(IHouseholdBL baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion


        /// <summary>
        /// Lấy dữ liệu phân trang
        /// </summary>
        [HttpPost("list")]
        public async Task<IActionResult> GetPaging([FromBody] DictionaryPagingItem pagingItem)
        {
            try
            {
                // Gọi đến Business Layer
                var result = await _baseBL.GetPaging(pagingItem);
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
