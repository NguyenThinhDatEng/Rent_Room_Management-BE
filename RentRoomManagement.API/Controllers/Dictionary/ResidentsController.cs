using RentRoomManagement.BL.Tenant.Dictionary.ResidentBL;
using RentRoomManagement.Common.Entitites.Dictionary;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    public class ResidentsController : BasesController<ResidentEntity, ResidentEntity>
    {
        public ResidentsController(IResidentBL businessLayer) : base(businessLayer)
        {
        }
    }
}
