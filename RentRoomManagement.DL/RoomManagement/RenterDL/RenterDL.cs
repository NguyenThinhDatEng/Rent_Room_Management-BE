using Dapper;
using MySqlConnector;
using RentRoomManagement.Common.Entities.Dictionary;
using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Param;

namespace RentRoomManagement.DL.RoomManagement.RenterDL
{
    public class RenterDL : IRenterDL
    {
        public async Task<UserEntity?> LinkToBuilding(BuldingLinkingParam buldingLinkingParam)
        {
            using (var connection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                connection.Open();

                var userTableName = "users";
                string sql = $"SELECT u.{nameof(UserEntity.user_id)}, u.{nameof(UserEntity.user_name)} FROM {userTableName} u " +
                    "JOIN user_roles ur ON ur.user_id = u.user_id " +
                    $"JOIN rhm_building rb on u.user_id = rb.user_id " +
                    $"WHERE u.phone_number = @phoneNumber AND ur.role_id = {(int)Role.Inkeeper} AND rb.{nameof(BuildingEntity.building_code)} = @buildingCode";

                var param = new Dictionary<string, object>()
                {
                    {"phoneNumber", buldingLinkingParam.PhoneNumber ?? "" },
                    {"buildingCode", buldingLinkingParam.BuildingCode ?? "" },
                };

                var result = await connection.QueryFirstOrDefaultAsync<UserEntity>(sql, param);
                if (result != null)
                {
                    return result;
                }

            }
            return default(UserEntity);
        }
    }
}
