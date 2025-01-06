using MySqlConnector;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Entitites.RoomManangement;
using RentRoomManagement.Common.Query;

namespace RentRoomManagement.DL.RoomManagement
{
    public class HouseholdDL : BaseDL<HouseholdEntity, HouseholdDto>, IHouseholdDL
    {
        /// <summary>
        /// Lấy dữ liệu phân trang
        /// </summary>
        /// <typeparam name="TT"></typeparam>
        /// <param name="pagingItem"></param>
        /// <returns></returns>
        public override async Task<PagingResult> GetPaging(IDicPagingItem pagingItem)
        {
            MySqlConnection connection = new MySqlConnection(DatabaseContext.ConnectionString);
            await connection.OpenAsync();

            MySqlCommand command = new MySqlCommand("CALL Proc_Create_Household_List()", connection);
            _ = await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();

            return await base.GetPaging(pagingItem);
        }
    }
}
