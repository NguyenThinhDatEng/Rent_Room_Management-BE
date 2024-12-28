using RentRoomManagement.Common.Entitites.RoomSearch.RoomPost;

namespace RentRoomManagement.Common.Param
{
    public class FavoritePostParam
    {
        public Guid? favorite_post_id { get; set; }
        public Guid? user_id { get; set; }
        public Guid? room_post_id { get; set; }
    }
}
