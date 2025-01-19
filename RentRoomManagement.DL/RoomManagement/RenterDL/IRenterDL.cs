using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Param;

namespace RentRoomManagement.DL.RoomManagement.RenterDL
{
    public interface IRenterDL
    {
        public Task<UserDtoClient?> LinkToBuilding(LinkingParam buldingLinkingParam);
    }
}
