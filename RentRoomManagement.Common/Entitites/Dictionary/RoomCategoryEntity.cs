using RentRoomManagement.Common.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites
{
    [Table("rhm_room_category")]
    public class RoomCategoryEntity : BaseEntity
    {
        #region Property

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public Guid room_category_id { get; set; }

        /// <summary>
        /// Tên loại phòng
        /// </summary>
        [RequiredField(field: "Tên loại phòng")]
        public string room_category_name { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public double unit_price { get; set; }

        #endregion
    }
}
