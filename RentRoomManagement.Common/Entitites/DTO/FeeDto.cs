using RentRoomManagement.Common.Entitites.RoomManangement.fee;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.DTO
{
    [Table("fee_view")]
    public class FeeDto : FeeEntity
    {
        public Guid room_id { get; set; }

        /// <summary>
        /// Mã phòng
        /// </summary>
        public string room_code { get; set; }

        /// <summary>
        /// Mã hợp đồng
        /// </summary>
        public string contract_code { get; set; }
    }
}
