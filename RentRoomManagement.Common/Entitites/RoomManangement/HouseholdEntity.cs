using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.RoomManangement
{
    [Table("rhm_household")]
    public class HouseholdEntity
    {
        public Guid room_id { get; set; }

        public Guid resident_id { get; set; }

        public int member_count { get; set; } = 1;

        public int vehicle_count { get; set; } = 0;
    }
}
