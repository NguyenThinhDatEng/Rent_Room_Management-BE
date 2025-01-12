using Microsoft.AspNetCore.Authorization;
using RentRoomManagement.BL.Tenant.Dictonary.RoomCategoryBL;
using RentRoomManagement.Common.Entitites.Dictionary;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    [Authorize]
    public class RoomCategoriesController : BasesController<RoomCategoryEntity, RoomCategoryEntity>
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
