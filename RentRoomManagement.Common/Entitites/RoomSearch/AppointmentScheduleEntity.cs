using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("appointment_schedule")] // Tên bảng trong cơ sở dữ liệu
public class AppointmentScheduleEntity
{
    [Key] // Đánh dấu thuộc tính này là khóa chính
    public Guid appointment_schedule_id { get; set; } // tương ứng với appointment_schedule_id

    public Guid user_id { get; set; } // người tạo

    public DateTime? appointment_date { get; set; } // tương ứng với appointment_date, có thể null

    public string? appointment_address { get; set; } // tương ứng với appointment_address

    public string? appointment_title { get; set; } // tương ứng với appointment_title, có thể null

    public string? appointment_note { get; set; } // tương ứng với appointment_note, có thể null

    public string? to_phone_number { get; set; }

    public string? to_user_name { get; set; }

    public string? appointment_time { get; set; }
}