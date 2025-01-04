using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entities.Dictionary
{
    [Table("rhm_building")]
    public class BuildingEntity
    {
        [Key]
        public Guid building_id { get; set; } // Khóa chính

        public string building_name { get; set; } // Tên nhà, tòa nhà, ..

        public string? building_address { get; set; } // địa chỉ

        public Guid user_id { get; set; } // Chủ sở hữu

        public int status { get; set; } // Trạng thái building: 0-Không hiển thị, 1-Đang hiển thị, 2-Không sử dụng
    }
}