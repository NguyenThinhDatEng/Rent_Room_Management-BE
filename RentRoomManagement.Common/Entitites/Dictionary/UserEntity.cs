using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Dictionary
{
    [Table("users")]
    public class UserEntity
    {
        public Guid user_id { get; set; }

        public string user_name { get; set; }
    }
}
