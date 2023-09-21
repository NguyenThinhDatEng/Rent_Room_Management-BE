using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.DL;

namespace RentRoomManagement.BL.Tenant.Dictonary
{
    public class UserBL : BaseBL<UserEntity>, IUserBL
    {
        public UserBL(IBaseDL<UserEntity> baseDL) : base(baseDL)
        {
        }
    }
}
