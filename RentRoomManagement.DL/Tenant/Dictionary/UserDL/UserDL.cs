using Dapper;
using MySqlConnector;
using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Param;
using System.Security.Cryptography;
using System.Text;

namespace RentRoomManagement.DL.Tenant.Dictionary
{
    public class UserDL : BaseDL<UserEntity, UserDtoClient>, IUserDL
    {
        public async Task<UserProfileDto?> GetUserProfile(Guid userId)
        {
            using var connection = new MySqlConnection(DatabaseContext.ConnectionString);
            var sql = $"SELECT user_name, phone_number, second_phone_number, user_email, user_facebook, user_zalo " +
                      $"FROM users WHERE user_id = @userId LIMIT 1";
            return await connection.QueryFirstOrDefaultAsync<UserProfileDto>(sql, new { userId });
        }

        public async Task<bool> UpdateUserProfile(Guid userId, UserProfileDto dto)
        {
            using var connection = new MySqlConnection(DatabaseContext.ConnectionString);
            var sql = "UPDATE users SET " +
                      "user_name = @user_name, " +
                      "phone_number = @phone_number, " +
                      "second_phone_number = @second_phone_number, " +
                      "user_email = @user_email, " +
                      "user_facebook = @user_facebook, " +
                      "user_zalo = @user_zalo " +
                      "WHERE user_id = @userId";
            var rows = await connection.ExecuteAsync(sql, new
            {
                dto.user_name,
                dto.phone_number,
                dto.second_phone_number,
                dto.user_email,
                dto.user_facebook,
                dto.user_zalo,
                userId
            });
            return rows > 0;
        }

        public async Task<(bool success, string message)> ChangePassword(Guid userId, ChangePasswordParam param)
        {
            using var connection = new MySqlConnection(DatabaseContext.ConnectionString);
            string currentHash = ComputeSHA256(param.CurrentPassword);
            var user = await connection.QueryFirstOrDefaultAsync<UserEntity>(
                "SELECT user_id FROM users WHERE user_id = @userId AND password_hash = @currentHash LIMIT 1",
                new { userId, currentHash });

            if (user == null)
                return (false, "Mật khẩu hiện tại không đúng.");

            string newHash = ComputeSHA256(param.NewPassword);
            var rows = await connection.ExecuteAsync(
                "UPDATE users SET password_hash = @newHash WHERE user_id = @userId",
                new { newHash, userId });

            return rows > 0 ? (true, "Đổi mật khẩu thành công.") : (false, "Đổi mật khẩu thất bại.");
        }

        private static string ComputeSHA256(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder();
            foreach (var b in bytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
