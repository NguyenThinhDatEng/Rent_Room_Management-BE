using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentRoomManagement.BL.Notification;
using RentRoomManagement.Common.Entitites.TDto;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Query;
using RentRoomManagement.Common.Resources;

namespace RentRoomManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private INotificationBL _notificationBL;

        public NotificationsController(INotificationBL notificationBL)
        {
            _notificationBL = notificationBL;
        }

        [HttpPost]
        public async Task<IActionResult> SendNoti(NotificationEntity notification)
        {
            var res = await _notificationBL.SendNoti(notification);
            return Ok(res);
        }

        /// <summary>
        /// API Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        [HttpPost("list")]
        public async Task<IActionResult> GetPaging([FromBody] DictionaryPagingItem pagingItem)
        {
            try
            {
                // Gọi đến Business Layer
                var result = await _notificationBL.GetPaging(pagingItem);
                // Thành công
                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, result);
                // Thất bại
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
