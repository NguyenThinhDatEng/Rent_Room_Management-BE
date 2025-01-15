using Dapper;
using MySqlConnector;
using RentRoomManagement.Common.Constants;
using RentRoomManagement.Common.Entitites.TDto;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Functions;
using RentRoomManagement.Common.Query;
using RentRoomManagement.Common.Resources;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using static Dapper.SqlMapper;

namespace RentRoomManagement.DL
{
    public class BaseDL<T, TDto> : IBaseDL<T, TDto>
    {
        #region Fields
        private string _tableName = TableNameMapper<T>();
        #endregion

        #region Method
        public static string TableNameMapper<TT>()
        {
            var type = typeof(TT);
            TableAttribute tableAttribute = (TableAttribute)Attribute.GetCustomAttribute(type, typeof(TableAttribute));
            var name = string.Empty;

            if (tableAttribute != null)
            {
                name = tableAttribute.Name;
            }

            return name;
        }

        private static EntityKey KeyMapper<TT>(TT entity)
        {
            var result = new EntityKey();
            var dataType = typeof(TT);
            var properties = dataType.GetProperties();
            // Tìm ra khóa của bảng
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(false);
                foreach (var attribute in attributes)
                {
                    if (attribute.GetType().Name == "KeyAttribute")
                    {
                        // Lấy tên trường key
                        result.Name = property.Name;

                        // Lấy giá trị của key
                        object propVal = property.GetValue(entity);
                        Guid uuid = new Guid(propVal.ToString());
                        result.Value = uuid;
                        return result;
                    }
                }
            }
            return result;
        }

        private static string KeyNameMapper<TT>()
        {
            Type dataType = typeof(TT);
            var properties = dataType.GetProperties();
            // Tìm ra khóa của bảng
            foreach (var property in properties)
            {
                var attributes = property.GetCustomAttributes(false);
                foreach (var attribute in attributes)
                {
                    if (attribute.GetType().Name == "KeyAttribute")
                    {
                        return property.Name;
                    }
                }
            }

            return "";
        }

        private string GetColumnNames<TT>()
        {
            var dataType = typeof(TT);
            var properties = dataType.GetProperties();
            string columnNames = string.Join(", ", properties.Select(p => p.Name));
            return columnNames;
        }

        private string GetColumnValues(string keyField)
        {
            Type dataType = typeof(T);
            var properties = dataType.GetProperties();

            string columnValues = string.Join(", ", properties.Where(p => !p.Name.Equals(keyField)).Select(p => $"{p.Name} = @{p.Name}"));
            return columnValues;
        }

        private string GetParameterNames<TT>()
        {
            Type dataType = typeof(TT);
            var properties = dataType.GetProperties();
            string parameterNames = string.Join(", ", properties.Select(p => "@" + p.Name));
            return parameterNames;
        }

        private void AddParameters<TT>(MySqlCommand command, TT data)
        {
            Type dataType = typeof(TT);
            var properties = dataType.GetProperties();

            foreach (var property in properties)
            {
                object value = property.GetValue(data);
                command.Parameters.AddWithValue("@" + property.Name, value);
            }
        }

        /// <summary>
        /// Xử lý tên cột trước khi build query
        /// </summary>
        /// <param name="columnName"></param>
        public static string SanitizeColumn(string columnName)
        {
            // Chỉ chấp nhận chữ cái, chữ số và dấu gạch dưới
            string safeCol = new string(columnName.Where(c => char.IsLetterOrDigit(c) || c == '_').ToArray());
            return safeCol;
        }

        /// <summary>
        /// Build sort
        /// </summary>
        private string BuildSortClause(List<SortItem> sortItems)
        {
            StringBuilder orderByClause = new StringBuilder();

            foreach (var sortItem in sortItems)
            {
                string safeColumnName = SanitizeColumn(sortItem.Column);
                string orderBy = $"{safeColumnName} {(sortItem.IsAscending ? "ASC" : "DESC")}";
                orderByClause.Append(orderBy).Append(", ");
            }

            // Xóa dấu phẩy cuối cùng nếu có
            if (orderByClause.Length > 0)
            {
                orderByClause.Length -= 2;
            }

            return $"ORDER BY {orderByClause.ToString()}";
        }

        #region GET
        /// <summary>
        /// Lấy dữ liệu phân trang
        /// </summary>
        /// <typeparam name="TT"></typeparam>
        /// <param name="pagingItem"></param>
        /// <returns></returns>
        public virtual async Task<PagingResult> GetPaging(IDicPagingItem pagingItem)
        {
            MySqlConnection connection = new MySqlConnection(DatabaseContext.ConnectionString);
            await connection.OpenAsync();

            // Columns
            string columnsStr = "*";
            if (pagingItem.Columns?.Count > 0)
            {
                columnsStr = string.Join(", ", pagingItem.Columns);
            }

            // Conditions
            string whereClause = "";
            if (pagingItem.Filters != null && pagingItem.Filters.Any())
            {
                whereClause = BuildQuery.BuildWhereClause(pagingItem.Filters, pagingItem.OrGroup);
            }

            // sort
            string sortClause = "";
            if (pagingItem.Sorts != null && pagingItem.Sorts.Any())
            {
                sortClause = BuildSortClause(pagingItem.Sorts);
            }

            var result = new PagingResult();

            var tableName = TableNameMapper<TDto>();
            string query = $"SELECT {columnsStr} " +
                $"FROM {tableName} " +
                $"{whereClause} " +
                $"{sortClause} " +
                $"LIMIT {pagingItem.Skip}, {pagingItem.Take};";
            result.Data = await connection.QueryAsync<TDto>(query);

            string countQuery = $"SELECT COUNT(*) FROM {tableName} {whereClause}";
            result.TotalCount = await connection.QueryFirstAsync<int>(countQuery);

            await connection.CloseAsync();

            return result;
        }

        /// <summary>
        /// Lấy dữ liệu phân trang
        /// </summary>
        /// <typeparam name="TT"></typeparam>
        /// <param name="pagingItem"></param>
        /// <returns></returns>
        public async Task<List<TT>> GetAll<TT>(PagingItem pagingItem)
        {
            MySqlConnection connection = new MySqlConnection(DatabaseContext.ConnectionString);
            await connection.OpenAsync();

            // Columns
            string columnsStr = "*";
            if (pagingItem.Columns?.Count > 0)
            {
                columnsStr = string.Join(", ", pagingItem.Columns);
            }

            // Conditions
            string whereClause = "";
            if (pagingItem.Filters?.Count > 0)
            {
                whereClause = BuildQuery.BuildWhereClause(pagingItem.Filters);
            }

            // sort
            string sortClause = "";
            if (pagingItem.Sorts != null && pagingItem.Sorts.Any())
            {
                sortClause = BuildSortClause(pagingItem.Sorts);
            }

            var tableName = TableNameMapper<TT>();
            if (string.IsNullOrEmpty(tableName))
            {
                throw new Exception("Thiếu cấu hình tên bảng");
            }

            string query = $"SELECT {columnsStr} FROM {tableName} " +
                $"{whereClause} " +
                $"{sortClause}";

            var result = await connection.QueryAsync<TT>(query) as List<TT>;

            await connection.CloseAsync();

            return result;
        }

        /// <summary>
        /// Lấy thông tin toàn bộ bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        public async Task<List<TDto>> GetAllRecords()
        {
            // Khởi tạo biến kết quả trả về
            var records = new List<TDto>();

            var connection = new MySqlConnection(DatabaseContext.ConnectionString);
            await connection.OpenAsync();

            // Thực hiện chèn dữ liệu vào bảng
            var sql = $"SELECT * FROM {TableNameMapper<TDto>()}";

            records = await connection.QueryAsync<TDto>(sql) as List<TDto>;

            await connection.CloseAsync();

            return records;
        }

        /// <summary>
        /// Lấy thông tin bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID bản ghi muốn lấy</param>
        /// <returns>Thông tin bản ghi theo ID</returns>
        /// Author: NVThinh (16/11/2022)
        public async Task<TDto> GetByID(Guid recordID)
        {
            var connection = new MySqlConnection(DatabaseContext.ConnectionString);
            await connection.OpenAsync();

            // Thực hiện chèn dữ liệu vào bảng
            var sql = $"SELECT * FROM {TableNameMapper<TDto>()} WHERE {KeyNameMapper<TDto>()} = @id;";
            var param = new Dictionary<string, object>()
            {
                {"id", recordID }
            };
            var result = await connection.QueryFirstOrDefaultAsync<TDto>(sql, param);

            await connection.CloseAsync();

            return result;
        }

        /// <summary>
        /// API lấy mã record mới
        /// </summary>
        /// <returns>Mã record mới</returns>
        /// Author: NVThinh 9/1/2023
        public string GetNextCode()
        {

            // Chuẩn bị câu lệnh SQL
            string procedureName = String.Format(Procedure.GET_LAST_CODE, typeof(T).Name);

            //Khởi tạo kết nối DB MySQL
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                //Thực hiện gọi vào DB
                var lastCode = mySqlConnection.QueryFirstOrDefault<string>(procedureName, commandType: System.Data.CommandType.StoredProcedure);

                //Xử lý kết quả trả về
                string lastNum = Regex.Match(lastCode, @"(\d+)(?!.*\d)").Value;
                int lenOfLastNum = lastNum.Length; // chiều dài mã số

                // Mã không có bất kỳ ký tự số nào
                if (lastNum == "") return lastCode + '1';

                int numVal = Int32.Parse(lastNum); // Ép sang kiểu số
                int lastIndex = lastCode.LastIndexOf(lastNum); // Chỉ số xuất hiện "lastNum" tính từ cuối lên

                // Tạo mã số mới
                string newNumCode = (numVal + 1).ToString(); // mã số mới = mã số cũ + 1
                int lenOfNewNumCode = newNumCode.Length; // Lấy chiều dài mã số mới
                // Tạo mã số mới hoàn chỉnh
                if (lenOfNewNumCode < lenOfLastNum) // Ex: old code: 00023, new code: 24 => new code: '000' + '24'
                    newNumCode = lastCode.Substring(lastIndex, lenOfLastNum - lenOfNewNumCode) + newNumCode;

                // xử lý kết quả đầu ra
                // Xóa mã số cũ
                lastCode = lastCode.Remove(lastIndex, lenOfLastNum);
                // Insert mã số mới
                lastCode = lastCode.Insert(lastIndex, newNumCode);

                // Trả về
                return lastCode;
            }
        }
        #endregion

        #region POST
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        public virtual async Task<TDto?> InsertSync(T entity)
        {
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                // Thực hiện chèn dữ liệu vào bảng
                MySqlCommand command = new MySqlCommand($"INSERT INTO {_tableName} ({GetColumnNames<T>()}) " +
                    $"VALUES ({GetParameterNames<T>()})", mySqlConnection);

                // Thêm các tham số vào câu truy vấn
                AddParameters(command, entity);

                // Thực thi câu truy vấn
                var affectRows = command.ExecuteNonQuery();

                mySqlConnection.Close();

                if (affectRows > 0)
                {
                    var keyConfig = KeyMapper(entity);
                    var newEntity = await GetByID(keyConfig.Value ?? Guid.Empty);
                    return newEntity;
                }
                else
                {
                    return default;
                }
            }
        }

        /// <summary>
        /// Thêm mới bản ghi bất đồng bộ
        /// </summary>
        public async Task<TT?> InsertAsync<TT>(TT entity)
        {
            using var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);

            await mySqlConnection.OpenAsync();

            // Thực hiện chèn dữ liệu vào bảng
            MySqlCommand command = new MySqlCommand($"INSERT INTO {TableNameMapper<TT>()} ({GetColumnNames<TT>()}) " +
                $"VALUES ({GetParameterNames<TT>()})", mySqlConnection);

            // Thêm các tham số vào câu truy vấn
            AddParameters(command, entity);

            // Thực thi câu truy vấn
            var affectRows = command.ExecuteNonQuery();

            await mySqlConnection.CloseAsync();

            if (affectRows > 0)
            {
                return entity;
            }
            else
            {
                return default;
            }
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="recordIDs">Danh sách ID các bản ghi cần xóa</param>
        /// <returns>Object chứa các thông tin trả về client</returns>
        /// <author>NVThinh 10/1/2023</author>
        public ServiceResponse DeleteMultipleFixedAsset(List<Guid> recordIDs)
        {
            // Thiết lập tên procedure
            string procedureName = String.Format(Procedure.DELETE_MULTIPLE, typeof(T).Name);

            // Chuyển đổi list sang json
            var IDs = JsonSerializer.Serialize(recordIDs);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("IDs", IDs);

            //Khởi tạo kết nối DB MySQL
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                // Khởi tạo transaction
                var transaction = mySqlConnection.BeginTransaction();

                try
                {
                    // Thực hiện gọi vào DB
                    var numberOfAffectedRows = mySqlConnection.Execute(procedureName, parameters, commandType: System.Data.CommandType.StoredProcedure, transaction: transaction);

                    if (numberOfAffectedRows == recordIDs.Count)

                    {
                        // Cam kết thực hiện thành công
                        transaction.Commit();
                    }
                    else
                    {
                        // Thực hiện thất bại, khôi phục dữ liệu
                        transaction.Rollback();
                        return new ServiceResponse { Success = false, ErrorCode = (int)ErrorCode.BadRequest, Data = new List<string> { Errors.UserMsg_Fail_Delelte_Batch } };
                    }

                    return new ServiceResponse { Success = true };
                }
                catch (Exception ex)
                {
                    return new ServiceResponse { Success = false, Data = new List<string> { ex.Message }, ErrorCode = (int)ErrorCode.Exception };
                }
            }
        }

        #endregion

        #region Update
        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// Return: Trả về số bản ghi bị ảnh hưởng
        /// Author: NVThinh (04/09/2023)
        public int UpdateAsync(T entity)
        {
            var keyConfig = KeyMapper(entity);
            var columns = GetColumnValues(keyConfig.Name ?? "");
            try
            {
                using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
                {
                    mySqlConnection.Open();

                    // Thực hiện chèn dữ liệu vào bảng
                    MySqlCommand command = new MySqlCommand($"UPDATE {_tableName} SET {columns} WHERE {keyConfig.Name} = @id", mySqlConnection);

                    // Thêm các tham số vào câu truy vấn
                    AddUpdateParameters(command, entity, keyConfig.Value, keyConfig.Name ?? "");

                    // Thực thi câu truy vấn
                    var res = command.ExecuteNonQuery();

                    mySqlConnection.Close();

                    return res;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} with Table {_tableName} with Key {keyConfig.Name}", ex);
            }
        }

        private void AddUpdateParameters(MySqlCommand command, T entity, Guid? id, string keyField)
        {
            Type dataType = typeof(T);
            var properties = dataType.GetProperties();

            foreach (var property in properties)
            {
                if (!property.Name.Equals(keyField))
                {
                    object value = property.GetValue(entity);
                    command.Parameters.AddWithValue("@" + property.Name, value);
                }
            }

            command.Parameters.AddWithValue("@id", id);
        }
        #endregion

        #region DELETE

        /// <summary>
        /// API Xóa 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID bản ghi cần xóa</param>
        /// <returns>ID bản ghi được xóa</return
        /// <author>NVThinh 10/1/2023</author>
        public int DeleteByID(Guid recordID)
        {
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                // Thực hiện chèn dữ liệu vào bảng
                MySqlCommand command = new MySqlCommand($"DELETE FROM {_tableName} WHERE {KeyNameMapper<T>()} = @id", mySqlConnection);

                command.Parameters.AddWithValue("@id", recordID);

                // Thực thi câu truy vấn
                var res = command.ExecuteNonQuery();

                mySqlConnection.Close();

                return res;
            }
        }

        #endregion 

        /// <summary>
        /// Kiểm tra trùng mã
        /// </summary>
        /// <param name="recordCode">Mã bản ghi</param>
        /// <param name="recordID">ID bản ghi</param>
        /// <param name="idType">tên id của bảng</param>
        /// <returns>Boolean</returns>
        /// Author: NVThinh (21/11/2022)
        public bool CheckDuplicateCode(string recordCode, Guid recordID, string idType)
        {
            // Chuẩn bị câu lệnh MySQL
            string procedureName = String.Format(Procedure.GET_BY_CODE, typeof(T).Name);

            // Chuẩn bị tham số
            var parameters = new DynamicParameters();
            parameters.Add($"v_{typeof(T).Name}Code", recordCode);

            // Thực hiện gọi vào DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Tìm mã bản ghi trong DB
                var code = mySqlConnection.QueryFirstOrDefault<T>(procedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                // Xử lí kết quả trả về
                // Nếu mã chưa tồn tại 
                if (code == null)
                {
                    return false;
                }
                else
                {
                    // Nếu mã đã tồn tại so sánh xem ID truyền vào có trùng với ID lấy lên từ Database không
                    var property = typeof(T).GetProperty(idType);
                    if ((Guid)property.GetValue(code) == recordID)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        #endregion
    }
}
