using RentRoomManagement.BL.Tenant.Dictonary.RoomBL;
using RentRoomManagement.Common.Entitites.Dictionary;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    public class RoomsController : BasesController<RoomEntity>
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
