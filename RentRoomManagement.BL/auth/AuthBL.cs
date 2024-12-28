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
        public bool ValidateLogin(LoginParam param)
        {
            if (param == null || 
                string.IsNullOrEmpty(param.Account) || 
                string.IsNullOrEmpty(param.Password))
            {
                return false;
            }

            var authDL = new AuthDL();
            return authDL.ValidateLogin(param);
        }
    }
}
