using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Dictionary.Contract
{
    [Table("contract_view")]
    public class ContractDto : ContractEntity
    {
        // Mã phòng
        public string room_code { get; set; }
    }
}
