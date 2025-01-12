using Microsoft.AspNetCore.Authorization;
using RentRoomManagement.BL.Tenant.Dictionary.ResidentBL;
using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.Common.Entitites.DTO;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    [Authorize]
    public class ResidentsController : BasesController<ResidentEntity, ResidentDto>
    {
        public ResidentsController(IResidentBL businessLayer) : base(businessLayer)
        {
        }
    }
}
