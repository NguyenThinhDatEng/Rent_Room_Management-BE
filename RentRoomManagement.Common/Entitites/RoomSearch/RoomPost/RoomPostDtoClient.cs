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
    }
}
