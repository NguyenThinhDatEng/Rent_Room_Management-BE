using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites
{
    [Table("users")]
    public class User
    {
        [Key]
        public Guid user_id { get; set; } // tương ứng với user_id
        public string phone_number { get; set; } // tương ứng với phone_number
        public string second_phone_number { get; set; } // tương ứng với second_phone_number
        public string? password_hash { get; set; } // tương ứng với password_hash
        public string? user_email { get; set; } // tương ứng với user_email, có thể null
        public string? user_zalo { get; set; } // tương ứng với user_zalo, có thể null
        public string? user_facebook { get; set; } // tương ứng với user_facebook, có thể null
        public string? user_avatar { get; set; } // tương ứng với user_avatar, có thể null
    }
}
