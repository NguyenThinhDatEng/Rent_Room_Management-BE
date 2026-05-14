using RentRoomManagement.Common.Entities.Dictionary;
using RentRoomManagement.Common.Entitites.DTO;

namespace RentRoomManagement.DL.Tenant.Dictionary.BuildingDL
{
    public interface IBuildingDL : IBaseDL<BuildingEntity, BuildingDto>
    {
        Task<bool> SetActiveBuilding(Guid buildingId, Guid userId);
    }
}
