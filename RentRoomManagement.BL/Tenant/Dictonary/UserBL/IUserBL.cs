using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Param;

namespace RentRoomManagement.BL.Tenant.Dictonary
{
    public interface IUserBL : IBaseBL<UserEntity, UserDtoClient>
    {
        Task<UserProfileDto?> GetUserProfile(Guid userId);
        Task<bool> UpdateUserProfile(Guid userId, UserProfileDto dto);
        Task<(bool success, string message)> ChangePassword(Guid userId, ChangePasswordParam param);
    }
}
