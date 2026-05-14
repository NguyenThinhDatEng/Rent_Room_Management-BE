using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.RoomSearch.RoomPost;
using RentRoomManagement.Common.Param;

namespace RentRoomManagement.DL.RoomSearch
{
    public interface IRoomPostDL : IBaseDL<RoomPostDtoEdit, RoomPostDtoClient>
    {
        Task<Guid?> LovePost(FavoritePostParam param);

        Task<List<Guid>> FilterByCharacteristic(List<int> FilterVals);

        Task<List<RoomPostDtoClient>> GetFavoritePosts(Guid? userID);

        Task<LinkingAccountEntity> LinkToInnkeeper(LinkingParam linkingParam);

        void DeleteLinking(Guid roomSeekerId);

        Task<RoomPostDtoClient> GetNew(RoomPostParam roomPostParam);

        Task<int> UpdatePostStatus(PostStatusUpdatingParam param);
    }
}
