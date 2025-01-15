namespace RentRoomManagement.Common.Entitites.DTO
{
    public class UserDtoClient : User
    {
        public Guid? building_id { get; set; }

        public Guid? building_linking_id { get; set; }
    }
}
