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
    }
}
