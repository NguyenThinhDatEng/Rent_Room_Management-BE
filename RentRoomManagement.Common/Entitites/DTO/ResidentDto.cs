using RentRoomManagement.Common.Entitites.Dictionary;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.DTO
{
    [Table("resident_view")]
    public class ResidentDto : ResidentEntity
    {
        public string? room_code {  get; set; }
    }
}
