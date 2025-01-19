using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentRoomManagement.BL.RoomManagement.Expense;
using RentRoomManagement.Common.Param;

namespace RentRoomManagement.API.Controllers.RoomManagement
{
    [Authorize]
    public class ExpensesController : BasesController<ExpenseEntity, ExpenseEntity>
    {
        IExpenseBL _expenseBL;

        public ExpensesController(IExpenseBL baseBL) : base(baseBL)
        {
            _expenseBL = baseBL;
        }

        [HttpPost("statistic")]
        public async Task<IActionResult> GetExpenseStatistic(ExpenseStatisticParam statisticParam)
        {
            var res = _expenseBL.GetExpenseStatisticsAsync(statisticParam);
            return Ok(res);
        }
    }
}
