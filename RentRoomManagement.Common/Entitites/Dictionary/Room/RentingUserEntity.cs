using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.Dictionary.Room
{
    [Table("rhm_renting_user")]
    public class RentingUserEntity
    {
        [Key]
        public Guid renting_user_id { get; set; }
        public Guid resident_id { get; set; }
        public Guid renting_id { get; set; }
    }
}