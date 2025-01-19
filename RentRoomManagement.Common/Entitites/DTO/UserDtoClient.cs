namespace RentRoomManagement.Common.Entitites.DTO
{
    public class UserDtoClient : UserEntity
    {
        public Guid? building_id { get; set; }

        public Guid? building_linking_id { get; set; }

        public string? building_name { get; set; }

        public string? room_id { get; set; }

        public string? room_name { get; set; }

        public Guid? innkeeper_id { get; set; }
    }
}
