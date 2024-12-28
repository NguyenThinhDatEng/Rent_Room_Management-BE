using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.Common.Entitites.RoomSearch.RoomPost;

namespace RentRoomManagement.BL.Tenant.Action
{
    public interface IRentingBL : IBaseBL<RentingEntity, RentingEntity>
    {
        /// <summary>
        /// Lấy tất cả khách hàng thuê liên quan đến 1 phòng
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetRentingUsers(Guid recordId);

        /// <summary>
        /// Cập nhật thuê phòng
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public int UpdateAsync(RentingEntity entity, string userIds);

        /// <summary>
        /// Thanh toán
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Pay(RentingEntity entity);
    }
}
