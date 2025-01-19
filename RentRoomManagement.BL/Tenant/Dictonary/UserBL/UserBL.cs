using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.DL;

namespace RentRoomManagement.BL.Tenant.Dictonary
{
    public class UserBL : BaseBL<UserEntity, UserDtoClient>, IUserBL
    {
        public UserBL(IBaseDL<UserEntity, UserDtoClient> baseDL) : base(baseDL)
        {
        }
    }
}
