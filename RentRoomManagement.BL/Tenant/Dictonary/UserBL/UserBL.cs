using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.DTO;
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
    }
}
