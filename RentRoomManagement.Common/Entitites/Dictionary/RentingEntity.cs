using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Dictionary
{
    [Table("rhm_renting")]
    public class RentingEntity
    {
        [Key]
        public Guid renting_id { get; set; }
        public Guid room_id { get; set; }
        public decimal room_price { get; set; }
        public decimal room_deposit { get; set; }
        public decimal deposit_amount_paid { get; set; }
        public DateTime start_date { get; set; }
        public DateTime check_out_date { get; set; }
    }
}