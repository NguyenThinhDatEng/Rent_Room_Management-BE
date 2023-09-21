using RentRoomManagement.Common.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Dictionary
{
    [Table("rhm_room")]
    public class RoomEntity
    {
        #region Property

        /// <summary>
        /// ID phòng
        /// </summary>
        [Key]
        public Guid room_id { get; set; }

        /// <summary>
        /// ID loại phòng
        /// </summary>
        [RequiredField(field: "ID loại phòng")]
        public Guid room_category_id { get; set; }

        /// <summary>
        /// Tên phòng
        /// </summary>
        [RequiredField(field: "Tên phòng")]
        public string room_name { get; set; }

        /// <summary>
        /// Tên loại phòng
        /// </summary>
        [RequiredField(field: "Tên loại phòng")]
        public string room_category_name { get; set; }

        /// <summary>
        /// Trạng thái phòng
        /// </summary>
        public bool? state { get; set; }

        #endregion
    }
}
