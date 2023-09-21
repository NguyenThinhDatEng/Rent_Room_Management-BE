using RentRoomManagement.Common.Entitites;
using RentRoomManagement.BL.Tenant.Dictonary.RoomCategoryBL;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    public class RoomCategoriesController : BasesController<RoomCategoryEntity>
    {
        #region Field

        private IRoomCategoryBL _roomCategoryBL;

        #endregion

        #region Constructor

        public RoomCategoriesController(IRoomCategoryBL roomCategoryBL) : base(roomCategoryBL)
        {
            _roomCategoryBL = roomCategoryBL;
        }

        #endregion
    }
}
