using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Param;

namespace RentRoomManagement.BL.auth
{
    public interface IAuthBL
    {
        Task<UserDtoClient?> ValidateLogin(LoginParam loginParam);
        Task<UserDtoClient?> GetUserOAuth2(LoginParam loginParam);
    }
}
