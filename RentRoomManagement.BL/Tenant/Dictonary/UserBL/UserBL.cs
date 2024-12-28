using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.Common.Entitites.RoomSearch.RoomPost;
using RentRoomManagement.DL;

namespace RentRoomManagement.BL.Tenant.Dictonary
{
    public class UserBL : BaseBL<UserEntity, RoomPostDtoClient>, IUserBL
    {
        public UserBL(IBaseDL<UserEntity, RoomPostDtoClient> baseDL) : base(baseDL)
        {
        }
    }
}
