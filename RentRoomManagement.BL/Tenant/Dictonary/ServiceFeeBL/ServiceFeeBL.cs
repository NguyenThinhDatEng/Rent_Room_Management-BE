using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.DL.Tenant.Dictionary.ServiceFeeDL;

namespace RentRoomManagement.BL.Tenant.Dictionary.ServiceFeeBL
{
    public class ServiceFeeBL : BaseBL<ServiceFeeEntity, ServiceFeeEntity>, IServiceFeeBL
    {
        public ServiceFeeBL(IServiceFeeDL businessLayer) : base(businessLayer)
        {
        }
    }
}