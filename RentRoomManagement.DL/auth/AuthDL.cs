using Dapper;
using MySqlConnector;
using RentRoomManagement.Common.Entities.Dictionary;
using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.Common.Entitites.Dictionary.Room;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Functions;
using RentRoomManagement.Common.Param;
using RentRoomManagement.DL;
using RentRoomManagement.DL.auth;
using RentRoomManagement.DL.Tenant.Dictionary.BuildingDL;
using System.Security.Cryptography;
using System.Text;

namespace RentRoomManagement.BL
{
    public class AuthDL : IAuthDL
    {
        private readonly IBuildingDL _buildingDL;
        public AuthDL(IBuildingDL buildingDL)
        {
            _buildingDL = buildingDL;
        }


        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public async Task<UserDtoClient?> ValidateLogin(LoginParam loginParam)
        {
            using (var connection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                connection.Open();

                string hashedPassword = CalculateSHA256Hash(loginParam.Password);

                var accountField = nameof(UserEntity.account);
                string sql = $"SELECT u.*, rb.{nameof(BuildingEntity.building_id)} FROM users u " +
                    "JOIN user_roles ur ON ur.user_id = u.user_id " +
                    $"LEFT JOIN rhm_building rb on u.user_id = rb.user_id AND rb.{nameof(BuildingEntity.status)} = {(int)BuildingStatus.Using} " +
                    $"WHERE u.{accountField} = @username AND u.password_hash = @password AND ur.role_id = @roleID";

                var param = new Dictionary<string, object>()
                {
                    {"username", loginParam.Account ?? "" },
                    {"password", hashedPassword },
                    {"roleID", ((int)loginParam.Role).ToString() },
                };

                var result = await connection.QueryFirstOrDefaultAsync<UserDtoClient>(sql, param);

                if (result != null)
                {
                    result.password_hash = null;
                }

                return result;
            }
        }

        public static string CalculateSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public async Task<UserDtoClient?> GetUserOAuth2(LoginParam loginParam)
        {
            var tableName = "users";
            using (var connection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                connection.Open();

                var roomTable = BuildQuery.TableNameMapper<RoomEntity>();
                var buildingNameField = nameof(BuildingEntity.building_name);
                var buildingIdField = nameof(BuildingEntity.building_id);
                var userIdField = nameof(UserEntity.user_id);
                var roomIdField = nameof(RoomEntity.room_id);
                var roomNameField = nameof(RoomEntity.room_name);

                var str = loginParam.Role != Role.RoomSeeker ? $"rbl.{userIdField}" : "la.innkeeper_id";

                string sql = $"SELECT u.*, " +
                    $"{str} as innkeeper_id," +
                    $"rb.{buildingNameField}, " +
                    $"rr.{roomNameField} " +
                    $"from {tableName} u " +
                    "JOIN user_roles ur ON ur.user_id = u.user_id " +
                    $"LEFT JOIN rhm_building_linking rbl on u.user_id = rbl.user_id " +
                    $"LEFT JOIN {roomTable} rr on rr.{roomIdField} = rbl.{roomIdField} " +
                    $"LEFT JOIN rhm_building rb on rr.{buildingIdField} = rb.{buildingIdField} " +
                    $"LEFT JOIN linking_account la on u.{userIdField} = la.room_seeker_id " +
                    $"WHERE u.{nameof(UserEntity.user_oauth2_id)} = @id AND ur.role_id = @roleID";

                var param = new Dictionary<string, object>()
                {
                    {"id", loginParam.UserOauth2Id ?? "" },
                    {"roleID", ((int)loginParam.Role).ToString() },
                };

                var result = await connection.QueryFirstOrDefaultAsync<UserDtoClient>(sql, param);

                if (result == null)
                {
                    // Đăng ký user tương ứng
                    var userId = Guid.NewGuid();
                    sql = $"INSERT INTO {tableName}" +
                        $"({nameof(UserEntity.user_id)},{nameof(UserEntity.user_name)},{nameof(UserEntity.user_oauth2_id)},{nameof(UserEntity.user_email)}) " +
                        $"Value ('{userId}', '{loginParam.UserName}', '{loginParam.UserOauth2Id}', '{loginParam.UserEmail}')";

                    result = new UserDtoClient()
                    {
                        user_id = userId,
                        user_name = loginParam.UserName,
                        user_email = loginParam.UserEmail
                    };

                    var affectRows = await connection.ExecuteAsync(sql);


                    if (affectRows > 0)
                    {
                        sql = $"INSERT INTO user_roles({nameof(UserEntity.user_id)},role_id) Value ('{userId}',{(int)loginParam.Role})";
                        _ = await connection.ExecuteAsync(sql);

                        if ((loginParam.Role == Role.Inkeeper || loginParam.Role == Role.Renter))
                        {
                            // Thêm mới tòa nhà
                            var buldingId = Guid.NewGuid();
                            sql = $"INSERT INTO rhm_building" +
                                $"({nameof(BuildingEntity.building_id)},{nameof(BuildingEntity.status)},{nameof(BuildingEntity.user_id)},{nameof(BuildingEntity.building_name)}) " +
                                $"Value ('{buldingId}', {(int)BuildingStatus.Using}, '{userId}', 'Tòa nhà 01')";

                            _ = await connection.ExecuteAsync(sql);
                            result.building_id = buldingId;
                        }
                    }
                }

                return result;
            }
        }
    }
}
