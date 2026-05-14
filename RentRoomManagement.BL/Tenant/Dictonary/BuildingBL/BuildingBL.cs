using RentRoomManagement.Common.Entities.Dictionary;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.DL.Tenant.Dictionary.BuildingDL;

namespace RentRoomManagement.BL.Tenant.Dictonary.BuildingBL
{
    public class BuildingBL : BaseBL<BuildingEntity, BuildingDto>, IBuildingBL
    {
        private readonly IBuildingDL _buildingDL;

        public BuildingBL(IBuildingDL roomCategoryDL) : base(roomCategoryDL)
        {
            _buildingDL = roomCategoryDL;
        }

        public async Task<bool> SetActiveBuilding(Guid buildingId, Guid userId)
        {
            return await _buildingDL.SetActiveBuilding(buildingId, userId);
        }
    }
}
