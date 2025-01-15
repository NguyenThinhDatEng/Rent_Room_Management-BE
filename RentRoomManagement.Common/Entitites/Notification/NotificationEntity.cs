using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("notifications")] // Tên bảng trong cơ sở dữ liệu
public class NotificationEntity
{
    [Key] // Đánh dấu thuộc tính này là khóa chính
    public Guid notification_id { get; set; } // tương ứng với notification_id

    public Guid? from_user_id { get; set; } // tương ứng với from_user_id, có thể null

    public Guid? to_user_id { get; set; } // tương ứng với to_user_id, có thể null

    public string notification_title { get; set; }

    public string notification_message { get; set; } // tương ứng với notification_message

    public string notification_type { get; set; } // tương ứng với notification_type

    public DateTime? read_at { get; set; } // tương ứng với is_read, mặc định là false

    public DateTime? created_at { get; set; } = DateTime.UtcNow; // tương ứng với created_at, có thể null
}