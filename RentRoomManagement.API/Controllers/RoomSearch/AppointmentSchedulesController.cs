using Microsoft.AspNetCore.Authorization;
using RentRoomManagement.BL.Tenant.Dictionary.AppointmentScheduleBL;
using RentRoomManagement.Common.Entitites.DTO;

namespace RentRoomManagement.API.Controllers.RoomSearch
{
    [Authorize]
    public class AppointmentSchedulesController : BasesController<AppointmentScheduleEntity, AppointmentScheduleDto>
    {
        public AppointmentSchedulesController(IAppointmentScheduleBL AppointmentScheduleBL) : base(AppointmentScheduleBL)
        {
        }
    }
}
