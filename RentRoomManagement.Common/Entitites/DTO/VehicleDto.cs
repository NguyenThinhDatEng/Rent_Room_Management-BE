using RentRoomManagement.Common.Entitites.RoomManangement;

namespace RentRoomManagement.Common.Entitites.DTO
{
    public class VehicleDto : VehicleEntity
    {
        /// <summary>
        /// Phí gửi xe
        /// </summary>
        public decimal fee_price { get; set; }

        /// <summary>
        /// Mã chủ xe
        /// </summary>
        public string resident_code { get; set; }

        /// <summary>
        /// Tên chủ xe
        /// </summary>
        public string resident_name { get; set; }
    }
}
