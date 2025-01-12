using Microsoft.AspNetCore.Authorization;
using RentRoomManagement.BL;
using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.Common.Entitites.RoomSearch.RoomPost;

namespace RentRoomManagement.API.Controllers.Dictionary
{
    [Authorize]
    public class UsersController : BasesController<UserEntity, RoomPostDtoClient>
    {
        public UsersController(IBaseBL<UserEntity, RoomPostDtoClient> baseBL) : base(baseBL)
        {
        }
    }
}
