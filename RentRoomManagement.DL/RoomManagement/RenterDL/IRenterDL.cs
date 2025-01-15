using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.Common.Param;

namespace RentRoomManagement.DL.RoomManagement.RenterDL
{
    public interface IRenterDL
    {
        public Task<UserEntity?> LinkToBuilding(BuldingLinkingParam buldingLinkingParam);
    }
}
