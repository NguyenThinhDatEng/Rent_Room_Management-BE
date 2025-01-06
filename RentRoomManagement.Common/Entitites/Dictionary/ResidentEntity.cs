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
        public bool is_owner { get; set; } = false;
        public string? identity_number { get; set; }
        public int resident_gender { get; set; }
        public DateTime? resident_bod { get; set; }
        public string? resident_career { get; set; }
        public bool on_leave { get; set; } = false;

        /// <summary>
        /// id phòng
        /// </summary>
        public Guid? room_id { get; set; }

        /// <summary>
        /// id tòa nhà
        /// </summary>
        public Guid building_id { get; set; }
    }
}