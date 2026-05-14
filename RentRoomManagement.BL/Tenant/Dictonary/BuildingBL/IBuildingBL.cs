using RentRoomManagement.Common.Entities.Dictionary;
using RentRoomManagement.Common.Entitites.DTO;

namespace RentRoomManagement.BL.Tenant.Dictonary.BuildingBL
{
    public interface IBuildingBL : IBaseBL<BuildingEntity, BuildingDto>
    {
        Task<bool> SetActiveBuilding(Guid buildingId, Guid userId);
    }
}
