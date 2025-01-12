using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Param;

namespace RentRoomManagement.BL
{
    public class AuthBL
    {
        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public async Task<UserDtoClient?> ValidateLogin(LoginParam param)
        {
            if (param == null || 
                string.IsNullOrEmpty(param.Account) || 
                string.IsNullOrEmpty(param.Password))
            {
                return default;
            }

            var authDL = new AuthDL();
            return await authDL.ValidateLogin(param);
        }
    }
}
