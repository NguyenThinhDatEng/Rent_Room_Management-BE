using RentRoomManagement.Common.Entitites.Dictionary.Room;
using RentRoomManagement.DL.Tenant.Dictionary.RoomDL;

namespace RentRoomManagement.BL.Tenant.Dictonary.RoomBL
{
    public class RoomBL : BaseBL<RoomEntity, RoomDto>, IRoomBL
    {
        private IRoomDL _roomDL;

        public RoomBL(IRoomDL roomDL) : base(roomDL)
        {
            _roomDL = roomDL;
        }
    }
}
