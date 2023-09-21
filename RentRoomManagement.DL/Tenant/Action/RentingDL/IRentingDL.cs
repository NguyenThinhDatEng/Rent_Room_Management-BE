using RentRoomManagement.Common.Entitites.Action;
using RentRoomManagement.Common.Entitites.Dictionary;

namespace RentRoomManagement.DL.Tenant.Action
{
    public interface IRentingDL : IBaseDL<RentingEntity>
    {
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// Return: Trả về số bản ghi bị ảnh hưởng
        /// Author: NVThinh (15/09/2023)
        public int InsertAsync(RentingEntity entity);

        /// <summary>
        /// API Xóa 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID bản ghi cần xóa</param>
        /// <returns>ID bản ghi được xóa</return
        /// <author>NVThinh 10/1/2023</author>
        public int DeleteByID(Guid recordID);

        /// <summary>
        /// Cập nhật thuê phòng
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public int UpdateAsync(RentingEntity entity, string userIds);

        /// <summary>
        /// Lấy tất cả khách hàng thuê liên quan đến 1 phòng
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetRentingUsers(Guid recordId);

        /// <summary>
        /// Thanh toán
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Pay(RentingEntity entity);
    }
}
