using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.RoomSearch.RoomPost
{
    [Table("room_post_view")]
    public class RoomPostDtoClient : RoomPostEntity
    {
        /// <summary>
        /// Id bài viết yêu thích
        /// </summary>
        public Guid? favorite_post_id { get; set; }
        public Guid? province_id { get; set; }
        public Guid? district_id { get; set; }
        public Guid? ward_id { get; set; }
        public string? street_name { get; set; }
        public string? house_number { get; set; }
        public string? user_facebook { get; set; }
        public string? phone_number { get; set; }
        public string? second_phone_number { get; set; }
        public string? user_zalo { get; set; }
        public string? user_name { get; set; }
    }
}
