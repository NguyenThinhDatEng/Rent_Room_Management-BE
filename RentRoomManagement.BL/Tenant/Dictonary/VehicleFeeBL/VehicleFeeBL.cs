using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.DL.Tenant.Dictionary.VehicleFeeDL;

namespace RentRoomManagement.BL.Tenant.Dictonary.VehicleFeeBL
{
    public class VehicleFeeBL : BaseBL<VehicleFeeEntity, VehicleFeeEntity>, IVehicleFeeBL
    {
        public VehicleFeeBL(IVehicleFeeDL baseDL) : base(baseDL)
        {
        }
    }
}