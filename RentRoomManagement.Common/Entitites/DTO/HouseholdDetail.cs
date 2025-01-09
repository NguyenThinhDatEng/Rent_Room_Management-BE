using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.Common.Entitites.RoomManangement;

namespace RentRoomManagement.Common.Entitites.DTO
{
    public class HouseholdDetail
    {
        public List<ResidentEntity>? residents { get; set; }

        public List<VehicleEntity>? vehicles { get; set; }
    }
}
