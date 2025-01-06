using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Entitites.RoomManangement;
using RentRoomManagement.DL.RoomManagement;

namespace RentRoomManagement.BL.RoomManagement
{
    public class HouseholdBL : BaseBL<HouseholdEntity, HouseholdDto>, IHouseholdBL
    {
        public HouseholdBL(IHouseholdDL baseDL) : base(baseDL)
        {
        }
    }
}
