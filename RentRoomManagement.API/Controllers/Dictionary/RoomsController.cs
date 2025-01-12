using Microsoft.AspNetCore.Authorization;
using RentRoomManagement.BL.Tenant.Dictonary.RoomBL;
using RentRoomManagement.Common.Entitites.Dictionary.Room;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    [Authorize]
    public class RoomsController : BasesController<RoomEntity, RoomDto>
    {
        #region Field

        private IRoomBL _roomBL;

        #endregion

        #region Constructor

        public RoomsController(IRoomBL roomBL) : base(roomBL)
        {
            _roomBL = roomBL;
        }

        #endregion
    }
}
