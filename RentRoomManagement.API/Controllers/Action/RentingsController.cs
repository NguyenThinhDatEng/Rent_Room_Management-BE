using Microsoft.AspNetCore.Mvc;
using RentRoomManagement.BL.Tenant.Action;
using RentRoomManagement.Common.Entitites.Action;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Resources;

namespace RentRoomManagement.API.Controllers.Action
{
    public class RentingsController : BasesController<RentingEntity>
    {
        #region Field

        private IRentingBL _rentingBL;

        #endregion

        #region Constructor

        public RentingsController(IRentingBL rentingBL) : base(rentingBL)
        {
            _rentingBL = rentingBL;
        }

        #endregion

        #region Method
        /// <summary>
        /// Lấy tất cả khách hàng thuê liên quan đến 1 phòng
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        [HttpGet("renting-users/{recordID}")]
        public IActionResult GetRentingUsers(Guid recordId)
        {
            try
            {
                // Gọi đến Business Layer
                var list = _rentingBL.GetRentingUsers(recordId);
                // Thành công
                if (list != null)
                    return StatusCode(StatusCodes.Status200OK, list);
                // Thất bại
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResult
                {
                    ErrorCode = QLTSErrorCode.NotFound,
                    DevMsg = Errors.DevMsg_Not_Found,
                    UserMsg = Errors.UserMsg_Not_Found,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = QLTSErrorCode.Exception,
                    DevMsg = Errors.DevMsg_Exception,
                    UserMsg = Errors.UserMsg_Exception,
                    MoreInfo = new List<string> { ex.Message },
                });
            }
        }

        #region Put
        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// Return: Trả về số bản ghi bị ảnh hưởng
        /// Author: NVThinh (04/09/2023)
        [HttpPut("update")]
        public IActionResult UpdateAsync([FromBody] RentingEntity entity,[FromQuery] string userIds)
        {
            // Gọi đến Business Layer
            var numberOfRowAffected = _rentingBL.UpdateAsync(entity, userIds);
            // Nếu số dòng ảnh hưởng > 0 trả về code thành công
            if (numberOfRowAffected > 0)
            {
                return StatusCode(StatusCodes.Status200OK, entity);
            }
            // Nếu số dòng ảnh hưởng > 0 trả về code lỗi server
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        #endregion

        /// <summary>
        /// Thanh toán
        /// </summary>
        /// Return: Trả về số bản ghi bị ảnh hưởng
        /// Author: NVThinh (21/09/2023)
        [HttpPost("pay")]
        public IActionResult Pay([FromBody] RentingEntity entity)
        {
            // Gọi đến Business Layer
            var numberOfRowAffected = _rentingBL.Pay(entity);
            // Nếu số dòng ảnh hưởng > 0 trả về code thành công
            if (numberOfRowAffected > 0)
            {
                return StatusCode(StatusCodes.Status200OK, entity);
            }
            // Nếu số dòng ảnh hưởng > 0 trả về code lỗi server
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        #endregion
    }
}
