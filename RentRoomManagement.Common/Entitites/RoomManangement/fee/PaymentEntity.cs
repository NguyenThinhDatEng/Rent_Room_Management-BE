using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.RoomManangement.fee
{
    [Table("rhm_payment")]
    public class PaymentEntity
    {
        [Key]
        public Guid payment_id { get; set; }

        public Guid fee_id { get; set; }

        public DateTime payment_date { get; set; } = DateTime.Now;

        public decimal payment_amount { get; set; }
    }
}
