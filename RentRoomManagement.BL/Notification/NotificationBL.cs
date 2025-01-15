using RentRoomManagement.DL.Notification;

namespace RentRoomManagement.BL.Notification
{
    public class NotificationBL : BaseBL<NotificationEntity, NotificationEntity>, INotificationBL
    {
        private INotificationDL _notificationDL;
        public NotificationBL(INotificationDL notificationDL) : base(notificationDL)
        {
            _notificationDL = notificationDL;
        }

        public async Task<bool> SendNoti(NotificationEntity notification)
        {
            var res = await InsertAsync(notification);
            if (res != null)
            {
                return true;
            }
            return false;
        }

        public async Task ReadNoti(Guid notificationId)
        {
            await _notificationDL.ReadNoti(notificationId);
        }
    }
}
