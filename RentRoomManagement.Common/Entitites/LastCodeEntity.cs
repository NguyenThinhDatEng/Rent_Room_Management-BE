using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites
{
    [Table("last_code")]
    public class LastCodeEntity
    {
        public int code_value { get; set; }

        public string? pre_code { get; set; }

        public string table_name { get; set; }
    }
}
