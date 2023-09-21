using RentRoomManagement.Common.Entitites;
using RentRoomManagement.DL.Tenant.Dictionary.ServiceCategoryDL;

namespace RentRoomManagement.BL.Tenant.Dictonary.ServiceCategoryBL
{
    public class ServiceCategoryBL : BaseBL<ServiceCategoryEntity>, IServiceCategoryBL
    {
        #region Field

        private IServiceCategoryDL _serviveCategoryDL;

        #endregion

        public ServiceCategoryBL(IServiceCategoryDL serviceCategoryDL) : base(serviceCategoryDL)
        {
            _serviveCategoryDL = serviceCategoryDL;
        }
    }
}
