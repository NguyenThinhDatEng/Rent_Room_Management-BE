using RentRoomManagement.BL.RoomManagement.vehicleBL;
using RentRoomManagement.Common.Entitites.RoomManangement;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    public class VehiclesController : BasesController<VehicleEntity, VehicleEntity>
    {
        public VehiclesController(IVehicleBL businessLayer) : base(businessLayer)
        {
        }
    }
}
