using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.RoomSearch.RoomPost
{
    [Table("favorite_post")]
    public class FavoritePostEntity
    {
        [Key]
        public Guid favorite_post_id { get; set; }
        public Guid user_id { get; set; }
        public Guid room_post_id { get; set; }
    }
}
