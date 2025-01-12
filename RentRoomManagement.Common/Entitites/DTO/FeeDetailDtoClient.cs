namespace RentRoomManagement.Common.Entitites.DTO
{
    public class FeeDetailDtoClient
    {
        public string room_name { get; set; }

        public int room_area { get; set; }

        public double room_price { get; set; }

        public string? resident_name { get; set; }

        public int member_count { get; set; }

        public List<VehicleDto>? vehicles { get; set; }
    }
}
