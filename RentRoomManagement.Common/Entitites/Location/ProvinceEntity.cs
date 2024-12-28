using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Location
{
    [Table("provinces")]
    public class ProvinceEntity
    {
        [Key]
        public string province_id { get; set; }

        public string? province_name { get; set; }

        public string? province_type { get; set; }
    }
}
