using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites
{
    [Table("linking_account")]
    public class LinkingAccountEntity
    {
        public Guid? room_seeker_id {  get; set; }

        public Guid? innkeeper_id { get; set; }
    }
}
