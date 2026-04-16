using System.ComponentModel.DataAnnotations;

namespace RentRoomManagement.Common.Entitites.RoomManangement
{
    public class ServiceIndexEntity
    {
        [Key]
        public Guid service_fee_id { get; set; }
        public int? old_index { get; set; }
        public int? new_index { get; set; }
        public Guid? fee_id { get; set; }
    }
}
