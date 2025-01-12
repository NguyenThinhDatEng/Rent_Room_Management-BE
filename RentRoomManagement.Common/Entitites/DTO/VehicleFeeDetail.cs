namespace RentRoomManagement.Common.Entitites.DTO
{
    public class VehicleFeeDetail
    {
        public Guid room_id { get; set; }

        public string room_name { get; set; }

        public int room_area { get; set; }

        public double room_price { get; set; }

        public Guid resident_id { get; set; }

        public string resident_code { get; set; }

        public string resident_name { get; set; }

        public bool? is_owner { get; set; }

        public Guid? vehicle_id { get; set; }

        public string? license_plate { get; set; }

        public string? vehicle_brand { get; set; }

        public string? vehicle_type { get; set; }

        public decimal? fee_price { get; set; }
    }
}
