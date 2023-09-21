using RentRoomManagement.BL;
using RentRoomManagement.BL.Tenant.Dictonary.ServiceCategoryBL;
using RentRoomManagement.Common.Entitites;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    public class ServiceCategoriesController : BasesController<ServiceCategoryEntity>
    {
        #region Field

        private IServiceCategoryBL _serviceCategoryBL;

        #endregion

        #region Constructor

        public ServiceCategoriesController(IServiceCategoryBL serviceCategoryBL) : base(serviceCategoryBL)
        {
            _serviceCategoryBL = serviceCategoryBL;
        }

        #endregion
    }
}
