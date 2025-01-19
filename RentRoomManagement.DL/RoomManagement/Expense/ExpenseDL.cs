using MySqlConnector;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Param;
using System.Data;

namespace RentRoomManagement.DL.RoomManagement.Expense
{
    public class ExpenseDL : BaseDL<ExpenseEntity, ExpenseEntity>, IExpenseDL
    {
        public async Task<List<StatisticDto>> GetExpenseStatisticsAsync(ExpenseStatisticParam statisticParam)
        {
            var result = new List<StatisticDto>();

            using (var connection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                using (var command = new MySqlCommand("GetExpenseStatistics", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số vào command
                    command.Parameters.Add(new MySqlParameter("@p_year", statisticParam.Year));
                    command.Parameters.Add(new MySqlParameter("@p_month", statisticParam.Month));
                    command.Parameters.Add(new MySqlParameter("@p_week", statisticParam.Week));
                    command.Parameters.Add(new MySqlParameter("@p_day", statisticParam.Day));
                    command.Parameters.Add(new MySqlParameter("@p_hour", statisticParam.Hour));
                    command.Parameters.Add(new MySqlParameter("@user_id", statisticParam.userId));

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Đọc thống kê theo danh mục
                        while (await reader.ReadAsync())
                        {
                            var label = reader.GetString(reader.GetOrdinal("label"));
                            var totalAmount = reader.GetDouble(reader.GetOrdinal("total_amount"));

                            result.Add(new StatisticDto()
                            {
                                Label = label,
                                TotalAmount = totalAmount
                            });
                        }
                    }
                }
            }

            return result;
        }
    }
}
