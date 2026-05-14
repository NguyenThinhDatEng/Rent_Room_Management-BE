using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentRoomManagement.BL;
using RentRoomManagement.BL.Tenant.Dictonary;
using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Entitites.TDto;
using RentRoomManagement.Common.Enums;
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
    }
}
