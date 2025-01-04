using RentContractManagement.DL.Tenant.Dictionary.ContractDL;
using RentRoomManagement.BL;
using RentRoomManagement.Common.Entitites.Dictionary.Contract;

namespace RentContractManagement.BL.Tenant.Dictonary.ContractBL
{
    public class ContractBL : BaseBL<ContractEntity, ContractDto>, IContractBL
    {
        private IContractDL _ContractDL;

        public ContractBL(IContractDL ContractDL) : base(ContractDL)
        {
            _ContractDL = ContractDL;
        }
    }
}
