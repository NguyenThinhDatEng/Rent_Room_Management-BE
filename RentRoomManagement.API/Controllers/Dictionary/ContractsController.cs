using Microsoft.AspNetCore.Authorization;
using RentContractManagement.BL.Tenant.Dictonary.ContractBL;
using RentRoomManagement.Common.Entitites.Dictionary.Contract;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    [Authorize]
    public class ContractsController : BasesController<ContractEntity, ContractDto>
    {
        public ContractsController(IContractBL ContractBL) : base(ContractBL)
        {
        }
    }
}
