using RentRoomManagement.Common.Entitites.Action;
using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.DL.Tenant.Action;
using System.Text.Json;

namespace RentRoomManagement.BL.Tenant.Action
{
    public class RentingBL : BaseBL<RentingEntity>, IRentingBL
    {
        private IRentingDL _rentingDL;

        public RentingBL(IRentingDL rentingDL) : base(rentingDL)
        {
            _rentingDL = rentingDL;
        }

        /// <summary>
        /// Lấy tất cả khách hàng thuê liên quan đến 1 phòng
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetRentingUsers(Guid recordId)
        {
            return _rentingDL.GetRentingUsers(recordId);
        }

        /// <summary>
        /// Cập nhật thuê phòng
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public int UpdateAsync(RentingEntity entity, string userIds)
        {
            return _rentingDL.UpdateAsync(entity, userIds);
        }

        /// <summary>
        /// Thanh toán
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Pay(RentingEntity entity)
        {
            return _rentingDL.Pay(entity);
        }
    }
}
