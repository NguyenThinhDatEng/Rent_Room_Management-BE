using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.RoomManangement.fee
{
    [Table("rhm_fee")]
    public class FeeEntity
    {
        [Key]
        public Guid fee_id { get; set; }

        public Guid contract_id { get; set; }

        /// <summary>
        /// Từ ngày
        /// </summary>
        public DateTime from_date { get; set; }

        /// <summary>
        /// Đến ngày
        /// </summary>
        public DateTime expired_date { get; set; }

        public decimal electric_fee { get; set; } = decimal.Zero;

        public decimal water_fee { get; set; } = decimal.Zero;

        public decimal total_fee { get; set; } = decimal.Zero;

        public decimal received_fee { get; set; } = decimal.Zero;

        public int fee_status { get; set; }

        public DateTime created_date { get; set; }

        public DateTime updated_date { get; set; }
    }
}
