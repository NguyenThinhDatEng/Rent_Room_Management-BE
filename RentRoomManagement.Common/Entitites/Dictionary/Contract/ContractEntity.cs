using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Dictionary.Contract
{
    [Table("rhm_contract")]
    public class ContractEntity
    {
        [Key]
        public Guid contract_id { get; set; } // ID hợp đồng

        public string contract_code { get; set; } // ID hợp đồng

        public Guid room_id { get; set; } // ID phòng

        public decimal room_price { get; set; } // Giá phòng

        public decimal room_deposit { get; set; } // Tiền cọc

        public decimal deposit_amount_received { get; set; } // Số tiền cọc đã trả

        public DateTime? start_date { get; set; } // Ngày bắt đầu hợp đồng

        public DateTime? end_date { get; set; } // Ngày kết thúc hợp đồng

        public DateTime created_date { get; set; } = DateTime.Now; // Ngày tạo
    }
}