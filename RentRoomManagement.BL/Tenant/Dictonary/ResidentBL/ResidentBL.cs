using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.DL;
using RentRoomManagement.DL.Tenant.Dictionary.ResidentDL;

namespace RentRoomManagement.BL.Tenant.Dictionary.ResidentBL
{
    public class ResidentBL : BaseBL<ResidentEntity, ResidentDto>, IResidentBL
    {
        public ResidentBL(IResidentDL baseDL) : base(baseDL)
        {
        }
    }
}