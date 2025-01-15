using MySqlConnector;

namespace RentRoomManagement.DL.Notification
{
    public class NotificationDL : BaseDL<NotificationEntity, NotificationEntity>, INotificationDL
    {
        public async Task ReadNoti(Guid notificationId)
        {
            MySqlConnection connection = new MySqlConnection(DatabaseContext.ConnectionString);
            await connection.OpenAsync();

            var tableName = TableNameMapper<NotificationEntity>();
            MySqlCommand command = new MySqlCommand($"UPDATE {tableName} SET {nameof(NotificationEntity.read_at)} = @now WHERE {nameof(NotificationEntity.notification_id)} = @id;", connection);

            command.Parameters.AddWithValue("@now", DateTime.Now);
            command.Parameters.AddWithValue("@id", notificationId);

            _ = await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();
        }
    }
}
