using RentRoomManagement.Common.Entitites.RoomManangement;
using RentRoomManagement.DL.Tenant.Dictionary.ExpenseCategoryDL;

namespace RentRoomManagement.BL.Tenant.Dictionary.ExpenseCategoryBL
{
    public class ExpenseCategoryBL : BaseBL<ExpenseCategoryEntity, ExpenseCategoryEntity>, IExpenseCategoryBL
    {
        public ExpenseCategoryBL(IExpenseCategoryDL baseDL) : base(baseDL)
        {
        }
    }
}