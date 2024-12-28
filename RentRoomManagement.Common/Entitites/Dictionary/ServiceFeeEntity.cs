using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Dictionary
{
    [Table("rhm_service_fee")]
    public class ServiceFeeEntity
    {
        [Key]
        public Guid service_fee_id { get; set; }
        public string? service_fee_code { get; set; }
        public string? service_type { get; set; }
        public uint fee_price { get; set; }
        public byte price_unit { get; set; }
        public bool is_default { get; set; }
        public Guid building_id { get; set; }
    }
}
