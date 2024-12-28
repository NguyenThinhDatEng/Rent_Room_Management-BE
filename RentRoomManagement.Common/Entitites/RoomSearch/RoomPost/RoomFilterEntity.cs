using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.RoomSearch.RoomPost
{
    [Table("room_filter")]
    public class RoomFilterEntity
    {
        [Key]
        public Guid room_filter_id { get; set; }
        public Guid room_post_id { get; set; }
        public int filter_value { get; set; }
    }
}
