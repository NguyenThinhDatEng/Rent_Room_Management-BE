using RentRoomManagement.BL;
using RentRoomManagement.Common.Entitites.Dictionary;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    public class UsersController : BasesController<UserEntity>
    {
        public UsersController(IBaseBL<UserEntity> baseBL) : base(baseBL)
        {
        }
    }
}
