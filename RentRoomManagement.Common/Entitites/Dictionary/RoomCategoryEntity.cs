using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Dictionary
{
    [Table("rhm_room_category")]
    public class RoomCategoryEntity
    {
        [Key]
        public Guid room_category_id { get; set; }
        public string room_category_code { get; set; }
        public string room_category_name { get; set; }
        public decimal? room_price { get; set; }
        public int? room_area { get; set; }
        public int? no_of_bed_rooms { get; set; } = 1;

        /// <summary>
        /// id tòa nhà
        /// </summary>
        public Guid building_id { get; set; }
    }
}