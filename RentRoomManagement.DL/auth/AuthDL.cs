using Dapper;
using MySqlConnector;
using RentRoomManagement.Common.Entities.Dictionary;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Param;
using RentRoomManagement.DL;
using System.Security.Cryptography;
using System.Text;

namespace RentRoomManagement.BL
{
    public class AuthDL
    {
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

                string sql = $"SELECT u.*, rb.{nameof(BuildingEntity.building_id)} FROM users u " +
                    "JOIN user_roles ur ON ur.user_id = u.user_id " +
                    $"LEFT JOIN rhm_building rb on u.user_id = rb.user_id AND rb.{nameof(BuildingEntity.status)} = {(int)BuildingStatus.Using} " +
                    "WHERE u.phone_number = @username AND u.password_hash = @password AND ur.role_id = @roleID";

                var param = new Dictionary<string, object>()
                {
                    {"username", loginParam.Account ?? "" },
                    {"password", hashedPassword },
                    {"roleID", ((int)loginParam.Role).ToString() },
                };

                var result = await connection.QueryFirstAsync<UserDtoClient>(sql, param);

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
    }
}
