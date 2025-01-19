using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.DL.Tenant.Dictionary.AppointmentScheduleDL;

namespace RentRoomManagement.BL.Tenant.Dictionary.AppointmentScheduleBL
{
    public class AppointmentScheduleBL : BaseBL<AppointmentScheduleEntity, AppointmentScheduleDto>, IAppointmentScheduleBL
    {
        public AppointmentScheduleBL(IAppointmentScheduleDL baseDL) : base(baseDL)
        {
        }
    }
}