using RentRoomManagement.Common.Entities.Dictionary;
using RentRoomManagement.DL.Tenant.Dictionary.BuildingDL;

namespace RentRoomManagement.BL.Tenant.Dictonary.BuildingBL
{
    public class BuildingBL : BaseBL<BuildingEntity, BuildingEntity>, IBuildingBL
    {
        public BuildingBL(IBuildingDL roomCategoryDL) : base(roomCategoryDL)
        {
        }
    }
}
