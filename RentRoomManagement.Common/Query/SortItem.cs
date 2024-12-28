namespace RentRoomManagement.Common.Query
{
    public class SortItem
    {
        /// <summary>
        /// Tên cột
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// Sắp xếp theo chiều tăng hay giảm
        /// Mặc định là tăng
        /// </summary>
        public bool IsAscending { get; set; } = true;
    }
}
