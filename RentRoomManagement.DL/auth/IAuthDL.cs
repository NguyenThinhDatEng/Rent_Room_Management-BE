using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Param;

namespace RentRoomManagement.DL.auth
{
    public interface IAuthDL
    {
        Task<UserDtoClient?> ValidateLogin(LoginParam loginParam);
        Task<UserDtoClient?> GetUserOAuth2(LoginParam loginParam);
    }
}
