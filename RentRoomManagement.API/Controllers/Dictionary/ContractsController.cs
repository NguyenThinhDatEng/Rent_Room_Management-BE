using Microsoft.AspNetCore.Authorization;
using RentContractManagement.BL.Tenant.Dictonary.ContractBL;
using RentRoomManagement.API.Controllers;
using RentRoomManagement.Common.Entitites.Dictionary.Contract;

namespace RentContractManagement.API.Controllers.Dictionary
{
    [Authorize]
    public class ContractsController : BasesController<ContractEntity, ContractDto>
    {
        #region Field

        private IContractBL _ContractBL;

        #endregion

        #region Constructor

        public ContractsController(IContractBL ContractBL) : base(ContractBL)
        {
            _ContractBL = ContractBL;
        }

        #endregion
    }
}
