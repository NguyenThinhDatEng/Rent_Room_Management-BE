using Dapper;
using MySqlConnector;
using RentRoomManagement.Common.Entities.Dictionary;
using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.Dictionary.Room;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Param;

namespace RentRoomManagement.DL.RoomManagement.RenterDL
{
    public class RenterDL : IRenterDL
    {
        public async Task<UserDtoClient?> LinkToBuilding(LinkingParam buldingLinkingParam)
        {
            using (var connection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                connection.Open();

                var userTableName = "users";
                var buildingIdField = nameof(BuildingEntity.building_id);
                var roomCodeField = nameof(RoomEntity.room_code);
                var roomIdField = nameof(RoomEntity.room_id);
                string sql = $"SELECT u.{nameof(UserEntity.user_id)}, " +
                    $"u.{nameof(UserEntity.user_name)}, " +
                    $"rr.{roomIdField} " +
                    $"FROM {userTableName} u " +
                    $"JOIN user_roles ur ON ur.user_id = u.user_id AND ur.role_id = {(int)Role.Inkeeper} " +
                    $"JOIN rhm_building rb on u.user_id = rb.user_id " +
                    $"JOIN rhm_room rr on  rr.{buildingIdField} = rb.{buildingIdField} AND rr.{roomCodeField} = @roomCode " +
                    $"WHERE u.phone_number = @phoneNumber";

                var param = new Dictionary<string, object>()
                {
                    {"phoneNumber", buldingLinkingParam.PhoneNumber ?? "" },
                    {"roomCode", buldingLinkingParam.RoomCode ?? "" },
                };

                var result = await connection.QueryFirstOrDefaultAsync<UserDtoClient>(sql, param);
                if (result != null)
                {
                    return result;
                }

            }
            return default;
        }
    }
}
