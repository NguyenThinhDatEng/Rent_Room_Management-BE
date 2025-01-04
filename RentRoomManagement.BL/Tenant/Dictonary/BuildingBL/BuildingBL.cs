using RentRoomManagement.Common.Entities.Dictionary;
using RentRoomManagement.DL.Tenant.Dictionary.BuildingDL;

namespace RentRoomManagement.BL.Tenant.Dictonary.BuildingBL
{
    public class BuildingBL : BaseBL<BuildingEntity, BuildingEntity>, IBuildingBL
    {
        #region Field

        private IBuildingDL _roomCategoryDL;

        #endregion

        public BuildingBL(IBuildingDL roomCategoryDL) : base(roomCategoryDL)
        {
            _roomCategoryDL = roomCategoryDL;
        }
    }
}
