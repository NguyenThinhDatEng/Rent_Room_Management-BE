using RentRoomManagement.Common.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites
{
    [Table("rhm_service_category")]
    public class ServiceCategoryEntity : BaseEntity
    {
        #region Property

        /// <summary>
        /// ID loại dịch vụ
        /// </summary>
        [Key]
        public Guid? service_category_id { get; set; }

        /// <summary>
        /// Tên loại dịch vụ
        /// </summary>
        [RequiredField(field: "Tên loại dịch vụ")]
        public string? service_category_name { get; set; }

        #endregion
    }
}
