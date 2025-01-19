using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Query;
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
            try
            {
                await InsertAsync(notification);
                if (notification.is_related == true)
                {
                    var paging = new PagingItem();
                    paging.Filters.Add(
                        new FilterItem()
                        {
                            Field = nameof(LinkingAccountEntity.room_seeker_id),
                            Value = notification.to_user_id,
                            Operator = Common.Enums.FilterOperator.Equal
                        }
                    );
                    var linkingAcc = await _notificationDL.GetAll<LinkingAccountEntity>(paging);
                    if (linkingAcc.Any())
                    {
                        var newNoti = notification;
                        newNoti.to_user_id = linkingAcc[0].innkeeper_id;
                        await InsertAsync(notification);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

        public async Task ReadNoti(Guid notificationId)
        {
            await _notificationDL.ReadNoti(notificationId);
        }
    }
}
