using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.RoomManangement
{
    [Table("rhm_vehicle")]
    public class VehicleEntity
    {
        [Key]
        public Guid vehicle_id {  get; set; }

        public Guid resident_id { get; set; }

        public string vehicle_type { get; set; }

        public string license_plate { get; set; }

        public string color { get; set; }

        public string vehicle_brand { get; set; }
    }
}
