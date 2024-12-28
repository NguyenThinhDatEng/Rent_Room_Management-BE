using RentRoomManagement.BL.Tenant.Dictonary.VehicleFeeBL;
using RentRoomManagement.Common.Entitites.Dictionary;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    public class VehicleFeeController : BasesController<VehicleFeeEntity, VehicleFeeEntity>
    {
        public VehicleFeeController(IVehicleFeeBL businessLayer) : base(businessLayer)
        {
        }
    }
}
