using MySqlConnector;
using RentRoomManagement.Common.Param;
using RentRoomManagement.DL;

namespace RentRoomManagement.BL
{
    public class AuthDL
    {
        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public bool ValidateLogin(LoginParam param)
        {
            using (var connection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM authentication WHERE account = @username AND password = @password";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", param.Account);
                command.Parameters.AddWithValue("@password", param.Password);

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
    }
}
