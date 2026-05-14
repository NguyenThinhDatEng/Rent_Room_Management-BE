using RentRoomManagement.Common.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.RoomSearch.RoomPost
{
    [Table("room_post")]
    public class RoomPostEntity
    {
        [Key]
        public Guid room_post_id { get; set; }

        [Code]
        public int post_code { get; set; }

        public string? post_title { get; set; }

        public string? room_address { get; set; }
        public decimal? room_price { get; set; }

        /// <summary>
        /// Đơn vị tính giá phòng
        /// </summary>
        public int? room_price_unit { get; set; }

        public int? room_area { get; set; }
        public int? no_of_bed_rooms { get; set; }
        public DateTime? posted_date { get; set; }
        public string? room_characteristic { get; set; }
        public string? room_description { get; set; }
        public string? room_map { get; set; }
        public string? images { get; set; }

        /// <summary>
        /// Link video
        /// </summary>
        public string? video_link { get; set; }

        /// <summary>
        /// id người đăng
        /// </summary>
        public Guid? user_id { get; set; }

        public int? room_vehicle_limit { get; set; }

        public int? room_people_limit { get; set; }

        public int? room_gender { get; set; }

        public int? post_status { get; set; }

        /// <summary>
        /// Loại phòng cho thuê
        /// </summary>
        public int? room_type {  get; set; }

        public DateTime? created_date { get; set; }

        public string? reject_message { get; set; }
    }
}
