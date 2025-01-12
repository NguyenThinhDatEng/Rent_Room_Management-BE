using Microsoft.AspNetCore.Mvc;
using RentRoomManagement.BL.Tenant.RoomSearch;
using RentRoomManagement.Common.Entitites.RoomSearch.RoomPost;
using RentRoomManagement.Common.Entitites.TDto;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Param;
using RentRoomManagement.Common.Resources;

namespace RentRoomManagement.API.Controllers.RoomSearch
{
    public class RoomPostsController : BasesController<RoomPostDtoEdit, RoomPostDtoClient>
    {
        private IRoomPostBL _roomPostBL;

        public RoomPostsController(IRoomPostBL roomPostBL) : base(roomPostBL)
        {
            _roomPostBL = roomPostBL;
        }

        /// <summary>
        /// Yêu thích bài viết
        /// </summary>
        [HttpPost("favorite-post")]
        public async Task<IActionResult> LovePost([FromBody] FavoritePostParam param)
        {
            var result = await _roomPostBL.LovePost(param);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Bài viết đã yêu thích
        /// </summary>
        [HttpGet("my-favorite-posts/{userID}")]
        public async Task<IActionResult> GetFavoritePosts(Guid userID)
        {
            var result = await _roomPostBL.GetFavoritePosts(userID);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Lấy bài viết đã lưu hoặc đăng
        /// </summary>
        [HttpGet("my-posts/{userID}")]
        public async Task<IActionResult> GetMyPosts(Guid userID)
        {
            var result = await _roomPostBL.GetMyPosts(userID);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Phân trang theo bộ lọc
        /// </summary>
        [HttpPost("filter-list")]
        public async Task<IActionResult> GetPagingCustom([FromBody] RoomFilterParam param)
        {
            try
            {
                // Gọi đến Business Layer
                var result = await _roomPostBL.GetPagingCustom(param);
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

        /// <summary>
        /// Phân trang theo bộ lọc
        /// </summary>
        [HttpPost("location")]
        public async Task<IActionResult> SaveLocation([FromBody] RoomPostLocationEntity param)
        {
            try
            {
                // Gọi đến Business Layer
                await _roomPostBL.SaveLocation(param);
                return StatusCode(StatusCodes.Status200OK, true);
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
