using Microsoft.AspNetCore.Authorization;
using RentRoomManagement.BL;
using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.DTO;

namespace RentRoomManagement.API.Controllers
{
    [Authorize]
    public class UsersController : BasesController<UserEntity, UserDtoClient>
    {
        public UsersController(IBaseBL<UserEntity, UserDtoClient> baseBL) : base(baseBL)
        {
        }
    }
}
