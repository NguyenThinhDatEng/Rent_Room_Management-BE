using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Dictionary
{
    [Table("rhm_resident")]
    public class ResidentEntity
    {
        [Key]
        public Guid resident_id { get; set; }
        public string resident_code { get; set; }
        public string resident_name { get; set; }
        public string phone_number { get; set; }
        public bool is_owner { get; set; }
        public string identity_number { get; set; }
        public int resident_gender { get; set; }
        public DateTime resident_bod { get; set; }
        public string career { get; set; }
        public bool onLeave { get; set; }
    }
}