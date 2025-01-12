using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentRoomManagement.BL.RoomManagement;
using RentRoomManagement.Common.Entitites.TDto;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Query;
using RentRoomManagement.Common.Resources;

namespace RentRoomManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class HouseholdsController : ControllerBase
    {
        #region Field

        private IHouseholdBL _householdBL;

        #endregion

        #region Constructor

        public HouseholdsController(IHouseholdBL householdBL)
        {
            _householdBL = householdBL;
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
                var result = await _householdBL.GetPaging(pagingItem);
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

        /// <summary>
        /// Lấy dữ liệu chi tiết
        /// </summary>
        [HttpGet("{recordID}")]
        public async Task<IActionResult> GetByID(Guid recordID)
        {
            try
            {
                // Gọi đến Business Layer
                var result = await _householdBL.GetDetail(recordID);
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
