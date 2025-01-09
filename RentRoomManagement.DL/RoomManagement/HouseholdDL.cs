using MySqlConnector;
using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Entitites.RoomManangement;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Query;

namespace RentRoomManagement.DL.RoomManagement
{
    public class HouseholdDL : BaseDL<HouseholdEntity, HouseholdDto>, IHouseholdDL
    {
        /// <summary>
        /// Tạo dữ liệu cho bảng danh sách hộ gia đình
        /// </summary>
        public async Task CreateHouseholdList(Guid VehicleID)
        {
            MySqlConnection connection = new MySqlConnection(DatabaseContext.ConnectionString);
            await connection.OpenAsync();

            MySqlCommand command = new MySqlCommand("CALL Proc_Create_Household_List(@VehicleID)", connection);

            command.Parameters.AddWithValue("@VehicleID", VehicleID);

            _ = await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();
        }

        public async Task<HouseholdDetail> GetDetail(Guid roomID)
        {
            var result = new HouseholdDetail();

            MySqlConnection connection = new MySqlConnection(DatabaseContext.ConnectionString);
            await connection.OpenAsync();

            var residentPaging = new PagingItem();

            residentPaging.Filters.Add(new FilterItem()
            {
                Field = nameof(ResidentEntity.room_id),
                Value = roomID,
                Operator = FilterOperator.Equal
            });

            residentPaging.Sorts.Add(new SortItem() { 
                Column = nameof(ResidentEntity.resident_code),
            });

            var residents = await GetAll<ResidentEntity>(residentPaging);
            if (residents?.Count > 0)
            {
                var vehiclePaging = new PagingItem();
                vehiclePaging.Filters.Add(new FilterItem()
                {
                    Field = nameof(ResidentEntity.resident_id),
                    Value = residents.Select(x => x.resident_id).ToList(),
                    Operator = FilterOperator.IN
                });

                var vehicles = await GetAll<VehicleEntity>(vehiclePaging);

                result.residents = residents;
                result.vehicles = vehicles;
            }

            await connection.CloseAsync();

            return result;
        }
    }
}
