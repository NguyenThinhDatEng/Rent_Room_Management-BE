
using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.RoomSearch.RoomPost;
using RentRoomManagement.Common.Param;
using RentRoomManagement.Common.Query;

namespace RentRoomManagement.BL.Tenant.RoomSearch
{
    public interface IRoomPostBL : IBaseBL<RoomPostDtoEdit, RoomPostDtoClient>
    {
        /// <summary>
        /// Lấy ra danh sách bài viết đã đăng
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<PagingResult> GetMyPosts(Guid? userID);

        /// <summary>
        /// Yêu thích/Hủy yêu thích bài đăng
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<object> LovePost(FavoritePostParam param);

        Task<PagingResult> GetPagingCustom(RoomFilterParam pagingItem);

        Task<List<RoomPostDtoClient>> GetFavoritePosts(Guid? userID);

        Task<LinkingAccountEntity> LinkToInnkeeper(LinkingParam linkingParam);

        Task SaveLocation(RoomPostLocationEntity roomPostLocation);

        void DeleteLinking(Guid roomSeekerId);

        Task<RoomPostDtoClient> GetNew(RoomPostParam roomPostParam);
    }
}
