using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.RoomSearch.RoomPost
{
    [Table("room_post_location")]
    public class RoomPostLocationEntity
    {
        [Key]
        public Guid room_post_location_id { get; set; }
        public Guid province_id { get; set; }
        public Guid district_id { get; set; }
        public Guid ward_id { get; set; }
        public string? street_name { get; set; }
        public string? house_number { get; set; }
        public Guid room_post_id { get; set; }
    }
}
