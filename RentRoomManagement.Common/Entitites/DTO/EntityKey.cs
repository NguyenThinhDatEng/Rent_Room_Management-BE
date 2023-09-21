namespace RentRoomManagement.Common.Entitites.DTO
{
    public class EntityKey
    {
        /// <summary>
        /// Tên khóa chính
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Giá trị khóa chính
        /// </summary>
        public Guid? Value { get; set; }
    }
}
