using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Dictionary
{
    [Table("rhm_vehicle_fee")]
    public class VehicleFeeEntity
    {
        [Key]
        public Guid vehicle_fee_id { get; set; }  // Khóa chính

        public string? vehicle_fee_code { get; set; } // Mã phương tiện

        public string? vehicle_type { get; set; } // Tên phương tiện

        public uint fee_price { get; set; } // Mức giá gửi xe (unsigned int)

        public byte price_unit { get; set; } // Đơn vị
    }
}
