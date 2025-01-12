using Microsoft.AspNetCore.Authorization;
using RentRoomManagement.BL.Tenant.Dictonary.BuildingBL;
using RentRoomManagement.Common.Entities.Dictionary;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    [Authorize]
    public class BuildingsController : BasesController<BuildingEntity, BuildingEntity>
    {
        public BuildingsController(IBuildingBL businessLayer) : base(businessLayer)
        {
        }
    }
}
