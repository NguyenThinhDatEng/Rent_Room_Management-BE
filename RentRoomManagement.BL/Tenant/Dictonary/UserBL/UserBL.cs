using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Param;
using RentRoomManagement.DL;
using RentRoomManagement.DL.Tenant.Dictionary;

namespace RentRoomManagement.BL.Tenant.Dictonary
{
    public class UserBL : BaseBL<UserEntity, UserDtoClient>, IUserBL
    {
        private readonly IUserDL _userDL;

        public UserBL(IUserDL userDL) : base(userDL)
        {
            _userDL = userDL;
        }

        public async Task<UserProfileDto?> GetUserProfile(Guid userId)
        {
            return await _userDL.GetUserProfile(userId);
        }

        public async Task<bool> UpdateUserProfile(Guid userId, UserProfileDto dto)
        {
            return await _userDL.UpdateUserProfile(userId, dto);
        }

        public async Task<(bool success, string message)> ChangePassword(Guid userId, ChangePasswordParam param)
        {
            if (param == null ||
                string.IsNullOrWhiteSpace(param.CurrentPassword) ||
                string.IsNullOrWhiteSpace(param.NewPassword))
            {
                return (false, "Vui lòng điền đầy đủ thông tin.");
            }

            if (param.NewPassword.Length < 6)
                return (false, "Mật khẩu mới phải có ít nhất 6 ký tự.");

            return await _userDL.ChangePassword(userId, param);
        }
    }
}
