using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Dictionary.Room
{
    [Table("room_view")]
    public class RoomDto : RoomEntity
    {
        /// <summary>
        /// Mã loại phòng
        /// </summary>
        public string? room_category_code { get; set; }

        /// <summary>
        /// Loại phòng
        /// </summary>
        public string? room_category_name { get; set; }

        /// <summary>
        /// Số người thuê của phòng
        /// </summary>
        public int member_count { get; set; }
    }
}
