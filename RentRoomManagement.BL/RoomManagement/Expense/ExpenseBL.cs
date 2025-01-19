using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Param;
using RentRoomManagement.DL.RoomManagement.Expense;

namespace RentRoomManagement.BL.RoomManagement.Expense
{
    public class ExpenseBL : BaseBL<ExpenseEntity, ExpenseEntity>, IExpenseBL
    {
        IExpenseDL _expenseDL;
        public ExpenseBL(IExpenseDL baseDL) : base(baseDL)
        {
            _expenseDL = baseDL;
        }

        public async Task<List<StatisticDto>> GetExpenseStatisticsAsync(ExpenseStatisticParam statisticParam)
        {
            return await _expenseDL.GetExpenseStatisticsAsync(statisticParam);
        }
    }
}
