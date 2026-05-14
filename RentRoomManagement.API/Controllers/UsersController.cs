using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentRoomManagement.BL;
using RentRoomManagement.BL.Tenant.Dictonary;
using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Entitites.TDto;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Param;
using RentRoomManagement.Common.Resources;

namespace RentRoomManagement.API.Controllers
{
    [Authorize]
    public class UsersController : BasesController<UserEntity, UserDtoClient>
    {
        private readonly IUserBL _userBL;

        public UsersController(IUserBL userBL) : base(userBL)
        {
            _userBL = userBL;
        }

        /// <summary>
        /// Lấy thông tin liên hệ của người dùng
        /// </summary>
        [HttpGet("profile/{userId}")]
        public async Task<IActionResult> GetUserProfile([FromRoute] Guid userId)
        {
            try
            {
                var profile = await _userBL.GetUserProfile(userId);
                if (profile != null)
                    return StatusCode(StatusCodes.Status200OK, profile);
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResult
                {
                    ErrorCode = (int)ErrorCode.NotFound,
                    DevMsg = Errors.DevMsg_Not_Found,
                    UserMsg = Errors.UserMsg_Not_Found,
                });
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
        /// Cập nhật thông tin liên hệ của người dùng
        /// </summary>
        [HttpPut("profile/{userId}")]
        public async Task<IActionResult> UpdateUserProfile([FromRoute] Guid userId, [FromBody] UserProfileDto dto)
        {
            try
            {
                var updated = await _userBL.UpdateUserProfile(userId, dto);
                if (updated)
                    return StatusCode(StatusCodes.Status200OK);
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResult
                {
                    ErrorCode = (int)ErrorCode.NotFound,
                    DevMsg = Errors.DevMsg_Not_Found,
                    UserMsg = Errors.UserMsg_Not_Found,
                });
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
        /// Đổi mật khẩu người dùng
        /// </summary>
        [HttpPut("change-password/{userId}")]
        public async Task<IActionResult> ChangePassword([FromRoute] Guid userId, [FromBody] ChangePasswordParam param)
        {
            try
            {
                var (success, message) = await _userBL.ChangePassword(userId, param);
                if (success)
                    return StatusCode(StatusCodes.Status200OK, new { message });
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                {
                    ErrorCode = (int)ErrorCode.BadRequest,
                    DevMsg = message,
                    UserMsg = message,
                });
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
