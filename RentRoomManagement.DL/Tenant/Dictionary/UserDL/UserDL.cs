using Dapper;
using MySqlConnector;
using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.DTO;

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
    }
}
