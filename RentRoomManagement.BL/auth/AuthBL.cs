using RentRoomManagement.BL.auth;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Param;
using RentRoomManagement.DL.auth;

namespace RentRoomManagement.BL
{
    public class AuthBL : IAuthBL
    {
        private readonly IAuthDL _authDL;
        public AuthBL(IAuthDL authDL)
        {
            _authDL = authDL;
        }

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

            return await _authDL.ValidateLogin(param);
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public async Task<UserDtoClient?> GetUserOAuth2(LoginParam param)
        {
            if (param == null || string.IsNullOrEmpty(param.UserOauth2Id))
            {
                return default;
            }

            var user = await _authDL.GetUserOAuth2(param);
            return user;
        }
    }
}
