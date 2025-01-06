using RentRoomManagement.Common.Entitites.RoomManangement;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.DTO
{
    [Table("household_view")]
    public class HouseholdDto : HouseholdEntity
    {
        public string room_code { get; set; }

        public string? resident_code { get; set; }

        public string? resident_name { get; set; }
    }
}
