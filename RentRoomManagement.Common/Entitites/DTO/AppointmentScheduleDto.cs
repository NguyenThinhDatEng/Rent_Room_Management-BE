using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.DTO
{
    [Table("appointment_schedule_view")]
    public class AppointmentScheduleDto : AppointmentScheduleEntity
    {
        public string? user_avatar;
    }
}
