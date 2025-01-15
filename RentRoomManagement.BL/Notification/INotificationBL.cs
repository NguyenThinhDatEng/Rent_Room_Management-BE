namespace RentRoomManagement.BL.Notification
{
    public interface INotificationBL : IBaseBL<NotificationEntity, NotificationEntity>
    {
        Task<bool> SendNoti(NotificationEntity notification);
    }
}
