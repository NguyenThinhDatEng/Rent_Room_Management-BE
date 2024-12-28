using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Location
{
    [Table("location_view")]
    public class LocationDto
    {
        public Guid location_id { get; set; }

        public int location_code { get; set; }

        public string? location_name { get; set; }

        public int location_type { get; set; }

        public int? parent_code { get; set; }
    }
}
