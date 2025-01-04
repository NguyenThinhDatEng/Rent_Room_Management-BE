using RentRoomManagement.Common.Entitites.TDto;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Query;
using RentRoomManagement.DL;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RentRoomManagement.BL
{
    public class BaseBL<T, TDto> : IBaseBL<T, TDto>
    {
        #region Field

        private IBaseDL<T, TDto> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T, TDto> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        #region Get
        /// <summary>
        /// Lấy dữ liệu phân trang
        /// </summary>
        /// <param name="pagingItem"></param>
        /// <returns></returns>
        public virtual async Task<PagingResult> GetPaging(DictionaryPagingItem pagingItem)
        {
            if (pagingItem.SearchItem != null && !string.IsNullOrEmpty(pagingItem.SearchItem.Value))
            {
                var searchVal = pagingItem.SearchItem.Value;
                pagingItem.SearchItem.Columns.ForEach(x =>
                {
                    pagingItem.OrGroup.Add(new FilterItem()
                    {
                        Field = x,
                        Operator = FilterOperator.Contains,
                        Value = searchVal
                    });
                });
            }
            return await _baseDL.GetPaging(pagingItem);
        }

        /// <summary>
        /// Lấy thông tin toàn bộ bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Author: NVThinh (19/09/2023)
        public IEnumerable<T> GetAllRecords(string? keyWord)
        {
            return _baseDL.GetAllRecords(keyWord);
        }

        /// <summary>
        /// Lấy thông tin bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID bản ghi muốn lấy</param>
        /// <returns>Thông tin bản ghi theo ID</returns>
        public virtual async Task<TDto> GetByID(Guid recordID)
        {
            return await _baseDL.GetByID(recordID);
        }

        /// <summary>
        /// API lấy mã record mới
        /// </summary>
        /// <returns>Mã record mới</returns>
        /// Author: NVThinh 9/1/2023
        public string GetNextCode()
        {
            return _baseDL.GetNextCode();
        }
        #endregion

        #region Post
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// Return: Số bản ghi bị ảnh hưởng
        public async Task<TT?> InsertAsync<TT>(TT entity)
        {
            BeforeInsert(entity);

            return await _baseDL.InsertAsync(entity);
        }

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// Return: Số bản ghi bị ảnh hưởng
        public async Task<TDto?> InsertSync(T entity)
        {
            BeforeInsert(entity);

            var newEntity = await _baseDL.InsertSync(entity);

            await AfterInsertSync(entity);

            return newEntity;
        }

        protected void BeforeInsert<TT>(TT entity)
        {
            EnsureKeyValue(entity);
        }

        /// <summary>
        /// Đảm bảo khóa chính luôn có dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void EnsureKeyValue<TT>(TT entity)
        {
            // Kiểm tra xem entity có null hay không
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // Lấy kiểu của entity
            Type entityType = typeof(TT);

            // Tìm thuộc tính được đánh dấu bằng [Key]
            PropertyInfo keyProperty = entityType.GetProperties()
                .FirstOrDefault(prop => prop.GetCustomAttribute<KeyAttribute>() != null);

            // Kiểm tra xem thuộc tính khóa chính có tồn tại không
            if (keyProperty == null)
                throw new InvalidOperationException($"Không tìm thấy thuộc tính khóa chính được đánh dấu bằng [Key] trong kiểu {entityType.Name}");

            // Lấy giá trị của thuộc tính khóa chính
            var keyValue = keyProperty.GetValue(entity);

            // Kiểm tra xem giá trị khóa chính có hợp lệ hay không
            if (keyValue == null || keyValue.Equals(Activator.CreateInstance(keyProperty.PropertyType)))
            {
                // Gán giá trị ngẫu nhiên cho khóa chính
                if (keyProperty.PropertyType == typeof(Guid))
                {
                    keyProperty.SetValue(entity, Guid.NewGuid());
                }
                else if (keyProperty.PropertyType == typeof(int))
                {
                    // Gán giá trị ngẫu nhiên cho kiểu int (ví dụ từ 1 đến 10000)
                    Random random = new Random();
                    keyProperty.SetValue(entity, random.Next(1, 10001));
                }
                else
                {
                    throw new InvalidOperationException($"Không hỗ trợ kiểu khóa chính: {keyProperty.PropertyType.Name}");
                }
            }
        }

        protected virtual async Task AfterInsertSync(T entity) { }
        #endregion

        #region Update
        /// <summary>
        /// Cập nhật bản ghi
        /// </summary>
        /// Return: Trả về số bản ghi bị ảnh hưởng
        /// Author: NVThinh (04/09/2023)
        public int UpdateAsync(T entity)
        {
            return _baseDL.UpdateAsync(entity);
        }
        #endregion

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="recordIDs">Danh sách ID các bản ghi cần xóa</param>
        /// <returns>Object chứa các thông tin trả về client</returns>
        /// <author>NVThinh 10/1/2023</author>
        public ServiceResponse DeleteMultipleFixedAsset(List<Guid> recordIDs)
        {
            return _baseDL.DeleteMultipleFixedAsset(recordIDs);
        }

        /// <summary>
        /// API Xóa 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID bản ghi cần xóa</param>
        /// <returns>ID bản ghi được xóa</return
        /// <author>NVThinh 10/1/2023</author>
        public int DeleteByID(Guid recordID)
        {
            return _baseDL.DeleteByID(recordID);
        }

        /// <summary>
        /// Kiểm tra trùng mã
        /// </summary>
        /// <param name="recordCode">Mã bản ghi</param>
        /// <param name="recordID">ID bản ghi</param>
        /// <returns>Boolean</returns>
        /// Created by: NVThinh (21/11/2022)
        public bool CheckDuplicateCode(string recordCode, Guid recordID, string idType)
        {
            return _baseDL.CheckDuplicateCode(recordCode, recordID, idType);
        }



        #endregion
    }
}
