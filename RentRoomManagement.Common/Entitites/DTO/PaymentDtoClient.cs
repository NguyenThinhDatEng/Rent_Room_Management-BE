using RentRoomManagement.Common.Entitites.RoomManangement.fee;

namespace RentRoomManagement.Common.Entitites.DTO
{
    public class PaymentDtoClient
    {
        public string? resident_code {  get; set; }
        public string? resident_name {  get; set; }

        public List<PaymentEntity>? payments { get; set; }
    }
}
