namespace RentRoomManagement.DL.Notification
{
    public interface INotificationDL : IBaseDL<NotificationEntity, NotificationEntity>
    {
        Task ReadNoti(Guid notificationId);
    }
}
