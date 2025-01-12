using Microsoft.AspNetCore.Authorization;
using RentRoomManagement.BL.RoomManagement.vehicleBL;
using RentRoomManagement.Common.Entitites.RoomManangement;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    [Authorize]
    public class VehiclesController : BasesController<VehicleEntity, VehicleEntity>
    {
        public VehiclesController(IVehicleBL businessLayer) : base(businessLayer)
        {
        }
    }
}
