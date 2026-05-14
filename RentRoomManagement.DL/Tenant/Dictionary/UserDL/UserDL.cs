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
    }
}
