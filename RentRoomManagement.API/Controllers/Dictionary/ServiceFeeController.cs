using Microsoft.AspNetCore.Authorization;
using RentRoomManagement.BL.Tenant.Dictionary.ServiceFeeBL;
using RentRoomManagement.Common.Entitites.Dictionary;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    [Authorize]
    public class ServiceFeeController : BasesController<ServiceFeeEntity, ServiceFeeEntity>
    {
        public ServiceFeeController(IServiceFeeBL businessLayer) : base(businessLayer)
        {
        }
    }
}
