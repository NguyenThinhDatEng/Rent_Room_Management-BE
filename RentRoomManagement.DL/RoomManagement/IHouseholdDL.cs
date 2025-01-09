using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Entitites.RoomManangement;

namespace RentRoomManagement.DL.RoomManagement
{
    public interface IHouseholdDL : IBaseDL<HouseholdEntity, HouseholdDto>
    {
        public Task CreateHouseholdList(Guid VehicleID);

        public Task<HouseholdDetail> GetDetail(Guid roomID);
    }
}
