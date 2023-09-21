using RentRoomManagement.Common.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Dictionary
{
    [Table("rhm_user")]
    public class UserEntity : BaseEntity
    {
        #region Property

        /// <summary>
        /// ID người thuê
        /// </summary>
        [Key]
        public Guid user_id { get; set; }

        /// <summary>
        /// Họ tên người thuê
        /// </summary>
        [RequiredField(field: "Họ tên")]
        public string user_name { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        [RequiredField(field: "Giới tính")]
        public int gender { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [RequiredField(field: "Số điện thoại")]
        public string phone_number { get; set; }

        /// <summary>
        /// Số căn cước công dân
        /// </summary>
        public string? identifier_number { get; set; }
        
        /// <summary>
        /// Số căn cước công dân
        /// </summary>
        public int is_renting { get; set; }

        #endregion
    }
}
