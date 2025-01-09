using RentRoomManagement.Common.Entitites.RoomManangement;
using RentRoomManagement.DL.RoomManagement.VehicleDL;

namespace RentRoomManagement.BL.RoomManagement.vehicleBL
{
    public class VehicleBL : BaseBL<VehicleEntity, VehicleEntity>, IVehicleBL
    {
        public VehicleBL(IVehicleDL vehicleDL) : base(vehicleDL)
        {
        }
    }
}