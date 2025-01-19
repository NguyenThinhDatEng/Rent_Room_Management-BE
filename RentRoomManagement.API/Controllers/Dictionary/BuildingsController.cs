using Microsoft.AspNetCore.Authorization;
using RentRoomManagement.BL.Tenant.Dictonary.BuildingBL;
using RentRoomManagement.Common.Entities.Dictionary;
using RentRoomManagement.Common.Entitites.DTO;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    [Authorize]
    public class BuildingsController : BasesController<BuildingEntity, BuildingDto>
    {
        public BuildingsController(IBuildingBL businessLayer) : base(businessLayer)
        {
        }
    }
}
