using RentRoomManagement.Common.Enums;

namespace RentRoomManagement.Common.Entitites.TDto
{
    public class ServiceResponse
    {
        /// <summary>
        /// Kết quả thành công hay thất bại
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Tên lỗi
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Dữ liệu phản hồi
        /// </summary>
        public object Data { get; set; }
    }
}
