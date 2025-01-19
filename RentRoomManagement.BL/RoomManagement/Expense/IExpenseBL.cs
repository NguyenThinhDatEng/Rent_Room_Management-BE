using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Param;

namespace RentRoomManagement.BL.RoomManagement.Expense
{
    public interface IExpenseBL : IBaseBL<ExpenseEntity, ExpenseEntity>
    {
        Task<List<StatisticDto>> GetExpenseStatisticsAsync(ExpenseStatisticParam statisticParam);
    }
}
