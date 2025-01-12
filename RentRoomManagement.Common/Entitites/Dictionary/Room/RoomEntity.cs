using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Dictionary.Room
{
    [Table("rhm_room")]
    public class RoomEntity
    {
        [Key]
        public Guid room_id { get; set; }
        public string room_code { get; set; }
        public string room_name { get; set; }
        public string? room_position { get; set; }
        public short room_area { get; set; }
        public decimal room_price { get; set; }
        public byte no_of_bed_rooms { get; set; }
        public Guid? room_category_id { get; set; }
        public Guid building_id { get; set; }
    }
}