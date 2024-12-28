using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.RoomSearch.RoomPost;
using RentRoomManagement.DL.Tenant.Dictionary.ServiceCategoryDL;

namespace RentRoomManagement.BL.Tenant.Dictonary.ServiceCategoryBL
{
    public class ServiceCategoryBL : BaseBL<ServiceCategoryEntity, RoomPostDtoClient>, IServiceCategoryBL
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
