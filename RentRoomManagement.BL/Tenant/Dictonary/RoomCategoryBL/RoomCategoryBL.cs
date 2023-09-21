using RentRoomManagement.Common.Entitites;
using RentRoomManagement.DL.Tenant.Dictionary.RoomCategoryDL;

namespace RentRoomManagement.BL.Tenant.Dictonary.RoomCategoryBL
{
    public class RoomCategoryBL : BaseBL<RoomCategoryEntity>, IRoomCategoryBL
    {
        #region Field

        private IRoomCategoryDL _roomCategoryDL;

        #endregion

        public RoomCategoryBL(IRoomCategoryDL roomCategoryDL) : base(roomCategoryDL)
        {
            _roomCategoryDL = roomCategoryDL;
        }
    }
}
