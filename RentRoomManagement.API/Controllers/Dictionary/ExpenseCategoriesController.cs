using Microsoft.AspNetCore.Authorization;
using RentRoomManagement.BL.Tenant.Dictionary.ExpenseCategoryBL;
using RentRoomManagement.Common.Entitites.RoomManangement;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    [Authorize]
    public class ExpenseCategoriesController : BasesController<ExpenseCategoryEntity, ExpenseCategoryEntity>
    {
        public ExpenseCategoriesController(IExpenseCategoryBL businessLayer) : base(businessLayer)
        {
        }
    }
}
