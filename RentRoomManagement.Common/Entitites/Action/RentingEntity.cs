using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Action
{
    [Table("rhm_renting")]
    public class RentingEntity : BaseEntity
    {
        [Key]
        public Guid renting_id { get; set; }

        public Guid user_id { get; set; }

        public Guid room_id { get; set; }

        public string? room_name { get; set; }

        public double price { get; set; }

        public double deposit { get; set; }
        /// <summary>
        /// Số lượng người trong phòng
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// Số tiền đã thanh toán
        /// </summary>
        public double amount_paid { get; set; }
        /// <summary>
        /// Số tiền cần thanh toán còn lại
        /// </summary>
        public double remaining_amount { get; set; }
        /// <summary>
        /// Ngày thuê
        /// </summary>
        public DateTime? room_rental_date { get; set; }
        /// <summary>
        /// Ngày trả tiền
        /// </summary>
        public DateTime? check_out_date { get; set; }
    }
}
