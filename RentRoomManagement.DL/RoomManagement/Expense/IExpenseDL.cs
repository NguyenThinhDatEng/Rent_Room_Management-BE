using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Param;

namespace RentRoomManagement.DL.RoomManagement.Expense
{
    public interface IExpenseDL : IBaseDL<ExpenseEntity, ExpenseEntity>
    {
        Task<List<StatisticDto>> GetExpenseStatisticsAsync(ExpenseStatisticParam statisticParam);
    }
}
