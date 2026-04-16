using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.RoomManangement
{
    [Table("rhm_room_linking")]
    public class RoomLinkingEntity
    {
        public Guid? user_id { get; set; }

        public Guid? room_id { get; set; }

        public Guid? innkeeper_id { get; set; }
    }
}
