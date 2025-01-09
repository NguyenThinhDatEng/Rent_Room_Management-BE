using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Entitites.RoomManangement;

namespace RentRoomManagement.BL.RoomManagement
{
    public interface IHouseholdBL : IBaseBL<HouseholdEntity, HouseholdDto>
    {
        public Task<HouseholdDetail> GetDetail(Guid roomID);
    }
}
