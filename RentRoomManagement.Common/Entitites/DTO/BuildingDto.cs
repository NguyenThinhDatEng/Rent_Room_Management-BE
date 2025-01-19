using RentRoomManagement.Common.Entities.Dictionary;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.DTO
{
    [Table("rhm_building_view")]
    public class BuildingDto : BuildingEntity
    {
        public Guid? province_id { get; set; }
        public Guid? district_id { get; set; }
        public Guid? ward_id { get; set; }

        public string? province_name { get; set; }
        public string? district_name { get; set; }
        public string? ward_name { get; set; }

    }
}
