using RentRoomManagement.Common.Entities.Dictionary;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.DL.Tenant.Dictionary.BuildingDL;

namespace RentRoomManagement.BL.Tenant.Dictonary.BuildingBL
{
    public class BuildingBL : BaseBL<BuildingEntity, BuildingDto>, IBuildingBL
    {
        public BuildingBL(IBuildingDL roomCategoryDL) : base(roomCategoryDL)
        {
        }
    }
}
