using RentRoomManagement.Common.Entitites.Action;
using RentRoomManagement.Common.Entitites.Dictionary;

namespace RentRoomManagement.BL.Tenant.Action
{
    public interface IRentingBL : IBaseBL<RentingEntity>
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
