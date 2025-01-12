using Microsoft.AspNetCore.Mvc;
using RentRoomManagement.BL.RoomManagement.FeeBL;
using RentRoomManagement.Common.Entitites.RoomManangement.fee;
using RentRoomManagement.Common.Entitites.TDto;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Param;
using RentRoomManagement.Common.Query;
using RentRoomManagement.Common.Resources;

namespace RentRoomManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FeesController : ControllerBase
    {
        #region Field
        private IFeeBL _feeBL;
        #endregion

        #region Constructor

        public FeesController(IFeeBL FeeBL)
        {
            _feeBL = FeeBL;
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
                var result = await _feeBL.GetPaging(pagingItem);
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
        /// Lấy dữ liệu phân trang
        /// </summary>
        [HttpGet("fees/{buildingID}")]
        public async Task<IActionResult> GenFees(Guid buildingID)
        {
            try
            {
                // Gọi đến Business Layer
                await _feeBL.GenerateFees(buildingID);
                return StatusCode(StatusCodes.Status200OK);
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
        /// 
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> pay([FromBody] PaymentEntity payment)
        {
            try
            {
                // Gọi đến Business Layer
                await _feeBL.Pay(payment);
                return StatusCode(StatusCodes.Status200OK);
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
        /// 
        /// </summary>
        [HttpPost("new")]
        public async Task<IActionResult> GetPaymentInfo([FromBody] PaymentParam payment)
        {
            // Gọi đến Business Layer
            var result = await _feeBL.GetPaymentInfo(payment);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// API Xóa 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID bản ghi cần xóa</param>
        /// <returns>ID bản ghi được xóa</return
        [HttpDelete("{recordID}")]
        public IActionResult Delete([FromRoute] Guid recordID)
        {
            try
            {
                // Gọi đến Business Layer
                var numberOfRowsAffected = _feeBL.DeleteByID(recordID);

                // Xử lý kết quả trả về
                if (numberOfRowsAffected > 0)
                    // Thành công
                    return StatusCode(StatusCodes.Status200OK, recordID);
                else
                    // Thất bại
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = (int)ErrorCode.BadRequest,
                        DevMsg = Errors.DevMsg_Bad_Request,
                        UserMsg = Errors.UserMsg_Fail,
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = (int)ErrorCode.Exception,
                    DevMsg = Errors.DevMsg_Exception,
                    UserMsg = Errors.UserMsg_Exception,
                    MoreInfo = new List<string> { ex.Message }
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
                var result = await _feeBL.GetDetail(recordID);
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
        /// Cập nhật bản ghi
        /// </summary>
        /// Return: Trả về số bản ghi bị ảnh hưởng
        [HttpPut]
        public IActionResult UpdateAsync([FromBody] FeeEntity entity)
        {
            // Gọi đến Business Layer
            var numberOfRowAffected = _feeBL.UpdateAsync(entity);
            // Nếu số dòng ảnh hưởng > 0 trả về code thành công
            if (numberOfRowAffected > 0)
            {
                return StatusCode(StatusCodes.Status200OK, entity);
            }
            // Nếu số dòng ảnh hưởng > 0 trả về code lỗi server
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
