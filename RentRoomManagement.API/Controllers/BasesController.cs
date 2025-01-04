using Microsoft.AspNetCore.Mvc;
using RentRoomManagement.BL;
using RentRoomManagement.Common.Entitites.TDto;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Query;
using RentRoomManagement.Common.Resources;

namespace RentRoomManagement.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T, TDto> : ControllerBase
    {
        #region Field

        private IBaseBL<T, TDto> _baseBL;

        #endregion

        #region Constructor

        public BasesController(IBaseBL<T, TDto> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method
        /// <summary>
        /// API Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Author: Nguyen Van Thinh 11/11/2022
        [HttpPost("list")]
        public async Task<IActionResult> GetPaging([FromBody] DictionaryPagingItem pagingItem)
        {
            try
            {
                // Gọi đến Business Layer
                var result = await _baseBL.GetPaging(pagingItem);
                // Thành công
                if (result != null)
                    return StatusCode(StatusCodes.Status200OK, result);
                // Thất bại
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResult
                {
                    ErrorCode = (int)QLTSErrorCode.NotFound,
                    DevMsg = Errors.DevMsg_Not_Found,
                    UserMsg = Errors.UserMsg_Not_Found,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = (int)QLTSErrorCode.Exception,
                    DevMsg = Errors.DevMsg_Exception,
                    UserMsg = Errors.UserMsg_Exception,
                    MoreInfo = new List<string> { ex.Message },
                });
            }
        }

        /// <summary>
        /// API Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Author: Nguyen Van Thinh 11/11/2022
        [HttpGet]
        public IActionResult GetAllRecords([FromQuery] string? keyWord = null)
        {
            try
            {
                // Gọi đến Business Layer
                var fixedAssetList = _baseBL.GetAllRecords(keyWord);
                // Thành công
                if (fixedAssetList != null)
                    return StatusCode(StatusCodes.Status200OK, fixedAssetList);
                // Thất bại
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResult
                {
                    ErrorCode = (int)QLTSErrorCode.NotFound,
                    DevMsg = Errors.DevMsg_Not_Found,
                    UserMsg = Errors.UserMsg_Not_Found,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = (int)QLTSErrorCode.Exception,
                    DevMsg = Errors.DevMsg_Exception,
                    UserMsg = Errors.UserMsg_Exception,
                    MoreInfo = new List<string> { ex.Message },
                });
            }
        }

        /// <summary>
        /// Lấy thông tin bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID bản ghi muốn lấy</param>
        /// <returns>Thông tin bản ghi theo ID</returns>
        /// Author: NVThinh (16/11/2022)
        [HttpGet("{recordID}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid recordID)
        {
            try
            {
                // Gọi đến Business Layer
                var record = await _baseBL.GetByID(recordID);

                // Xử lý kết quả trả về
                if (record != null)
                    // Thành công
                    return StatusCode(StatusCodes.Status200OK, record);
                // Thất bại
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResult
                {
                    ErrorCode = (int)QLTSErrorCode.NotFound,
                    DevMsg = Errors.DevMsg_Not_Found,
                    UserMsg = Errors.UserMsg_Not_Found
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = (int)QLTSErrorCode.Exception,
                    DevMsg = Errors.DevMsg_Exception,
                    UserMsg = Errors.UserMsg_Exception,
                    MoreInfo = new List<string> { ex.Message },
                });
            }

        }

        /// <summary>
        /// API lấy mã record mới
        /// </summary>
        /// <returns>Mã record mới</returns>
        /// Author: NVThinh 9/1/2023
        [HttpGet("newCode")]
        public IActionResult GetNextCode()
        {
            try
            {
                // Gọi đến Business Layer
                var newCode = _baseBL.GetNextCode();
                // Trả về cho Client
                return StatusCode(StatusCodes.Status201Created, newCode);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = (int)QLTSErrorCode.Exception,
                    DevMsg = Errors.DevMsg_Exception,
                    UserMsg = Errors.UserMsg_Exception,
                    MoreInfo = new List<string> { ex.Message },
                });
            }
        }

        #region POST
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// Created by: NVThinh (04/09/2023)
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] T entity)
        {
            // Gọi đến Business Layer
            var newEntity = await _baseBL.InsertSync(entity);
            // Nếu số dòng ảnh hưởng > 0 trả về code thành công
            if (newEntity != null)
            {
                return StatusCode(StatusCodes.Status200OK, newEntity);
            }
            // Nếu số dòng ảnh hưởng > 0 trả về code lỗi server
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="recordIDs">Danh sách ID các bản ghi cần xóa</param>
        /// <returns>Object chứa các thông tin trả về client</returns>
        /// <author>NVThinh 10/1/2023</author>
        [HttpPost("DeleteBatch")]
        public IActionResult DeleteMultipleFixedAsset([FromBody] List<Guid> recordIDs)
        {
            try
            {
                // Gọi đến Business Layer
                var serviceResponse = _baseBL.DeleteMultipleFixedAsset(recordIDs);

                // Xử lý kết quả trả về từ DB (GridReader)
                if (serviceResponse.Success)
                    return StatusCode(StatusCodes.Status200OK, recordIDs);
                else
                {
                    if (serviceResponse.ErrorCode == (int)QLTSErrorCode.BadRequest)
                        return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                        {
                            ErrorCode = (int)QLTSErrorCode.BadRequest,
                            DevMsg = Errors.DevMsg_Bad_Request,
                            UserMsg = Errors.UserMsg_Bad_Request,
                        });
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                        {
                            ErrorCode = serviceResponse.ErrorCode,
                            DevMsg = Errors.DevMsg_Exception,
                            UserMsg = Errors.UserMsg_Exception,
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = (int)QLTSErrorCode.Exception,
                    DevMsg = Errors.DevMsg_Exception,
                    UserMsg = Errors.UserMsg_Exception,
                    MoreInfo = new List<string> { ex.Message }
                });
            }
        }
        #endregion

        #region Put
        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// Return: Trả về số bản ghi bị ảnh hưởng
        /// Author: NVThinh (04/09/2023)
        [HttpPut]
        public virtual IActionResult UpdateAsync([FromBody] T entity)
        {
            // Gọi đến Business Layer
            var numberOfRowAffected = _baseBL.UpdateAsync(entity);
            // Nếu số dòng ảnh hưởng > 0 trả về code thành công
            if (numberOfRowAffected > 0)
            {
                return StatusCode(StatusCodes.Status200OK, entity);
            }
            // Nếu số dòng ảnh hưởng > 0 trả về code lỗi server
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        #endregion

        #region DELETE
        /// <summary>
        /// API Xóa 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID bản ghi cần xóa</param>
        /// <returns>ID bản ghi được xóa</return
        /// <author>NVThinh 10/1/2023</author>
        [HttpDelete("{recordID}")]
        public IActionResult DeleteFixedAsset([FromRoute] Guid recordID)
        {
            try
            {
                // Gọi đến Business Layer
                var numberOfRowsAffected = _baseBL.DeleteByID(recordID);

                // Xử lý kết quả trả về
                if (numberOfRowsAffected > 0)
                    // Thành công
                    return StatusCode(StatusCodes.Status200OK, recordID);
                else
                    // Thất bại
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = (int)QLTSErrorCode.BadRequest,
                        DevMsg = Errors.DevMsg_Bad_Request,
                        UserMsg = Errors.UserMsg_Fail,
                    });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = (int)QLTSErrorCode.Exception,
                    DevMsg = Errors.DevMsg_Exception,
                    UserMsg = Errors.UserMsg_Exception,
                    MoreInfo = new List<string> { ex.Message }
                });
            }
        }
        #endregion

        #endregion
    }
}
