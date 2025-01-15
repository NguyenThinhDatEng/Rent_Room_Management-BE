using RentRoomManagement.Common.Enums;

namespace RentRoomManagement.Common.Param
{
    public class LoginParam
    {
        public string? Account { get; set; }
        public string? Password { get; set; }

        /// <summary>
        /// Vai trò
        /// </summary>
        public Role Role { get; set; }

        public string? UserOauth2Id { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
    }
}
