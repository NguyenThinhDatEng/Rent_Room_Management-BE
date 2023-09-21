using Dapper;
using MySqlConnector;
using RentRoomManagement.Common.Constants;
using RentRoomManagement.Common.Entitites.Action;
using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.DL.Tenant.Dictionary;
using System.Data;

namespace RentRoomManagement.DL.Tenant.Action
{
    public class RentingDL : BaseDL<RentingEntity>, IRentingDL
    {
        private BaseDL<RentingEntity> baseDL;

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// Return: Trả về số bản ghi bị ảnh hưởng
        /// Author: NVThinh (15/09/2023)
        public override int InsertAsync(RentingEntity entity)
        {
            // Chuẩn bị tên Stored procedure
            string procedureName = "Proc_InsertRentingEntity";

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                MySqlCommand command = new MySqlCommand(procedureName, mySqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("roomId", entity.room_id);
                command.Parameters.AddWithValue("userId", entity.user_id);
                command.Parameters.AddWithValue("$price", entity.price);
                command.Parameters.AddWithValue("roomRentalDate", entity.room_rental_date);
                command.Parameters.AddWithValue("checkOutDate", entity.check_out_date);
                command.Parameters.AddWithValue("$deposit", entity.deposit);

                return command.ExecuteNonQuery();
            };
        }

        /// <summary>
        /// API Xóa 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID bản ghi cần xóa</param>
        /// <returns>ID bản ghi được xóa</return
        /// <author>NVThinh 10/1/2023</author>
        public int DeleteByID(Guid recordID)
        {
            // Chuẩn bị tên Stored procedure
            string procedureName = "Proc_DeleteRentingEntity";

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                MySqlCommand command = new MySqlCommand(procedureName, mySqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("id", recordID);

                command.ExecuteNonQuery();

                return 1;
            }
        }

        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// Return: Trả về số bản ghi bị ảnh hưởng
        /// Author: NVThinh (15/09/2023)
        public int UpdateAsync(RentingEntity entity, string userIds)
        {
            // Chuẩn bị tên Stored procedure
            string procedureName = "Proc_UpdateRentingEntity";

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                MySqlCommand command = new MySqlCommand(procedureName, mySqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("rentingId", entity.renting_id);
                command.Parameters.AddWithValue("price", entity.price);
                command.Parameters.AddWithValue("roomRentalDate", entity.room_rental_date);
                command.Parameters.AddWithValue("checkOutDate", entity.check_out_date);
                command.Parameters.AddWithValue("deposit", entity.deposit);
                command.Parameters.AddWithValue("userIds", userIds);

                command.ExecuteNonQuery();

                // Thực thi câu truy vấn
                return 1;
            }
        }

        /// <summary>
        /// Lấy tất cả khách hàng thuê liên quan đến 1 phòng
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetRentingUsers(Guid recordId) {
            // Chuẩn bị tên Stored procedure
            string procedureName = "Proc_GetRelatedUsers";

            // Khởi tạo biến kết quả trả về
            var records = new List<UserEntity>();

            //Khởi tạo kết nối DB MySQL
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Param truyền vào store
                object param = new
                {
                    rentingId = recordId
                };
                // Thực hiện gọi vào DB để chạy stored procedure với tham số đầu vào ở trên
                records = (List<UserEntity>)mySqlConnection.Query<UserEntity>(procedureName, commandType: System.Data.CommandType.StoredProcedure, param: param);
            }

            return records;
        }

        /// <summary>
        /// Thanh toán
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Pay(RentingEntity entity)
        {
            // Chuẩn bị tên Stored procedure
            string procedureName = "Proc_Pay";

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                MySqlCommand command = new MySqlCommand(procedureName, mySqlConnection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("rentingId", entity.renting_id);
                command.Parameters.AddWithValue("amountPaid", entity.amount_paid);

                // Thực thi câu truy vấn
                return command.ExecuteNonQuery();
            }
        }
    }
}