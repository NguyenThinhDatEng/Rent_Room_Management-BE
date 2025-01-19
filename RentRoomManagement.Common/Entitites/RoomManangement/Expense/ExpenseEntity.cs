using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("rhm_expense")] // Tên bảng trong cơ sở dữ liệu
public class ExpenseEntity
{
    [Key] // Đánh dấu thuộc tính này là khóa chính
    public Guid expense_id { get; set; } // tương ứng với expense_id

    public decimal? expense_amount { get; set; } // tương ứng với expense_amount

    public Guid? expense_category_id { get; set; } // tương ứng với expense_category_id, có thể null

    public string? expense_category_name { get; set; } // tương ứng với expense_category_name

    public string? expense_description { get; set; } // tương ứng với expense_description, có thể null

    public DateTime? expense_date { get; set; } // tương ứng với expense_date

    public DateTime? created_at { get; set; } // tương ứng với created_at, có thể null

    public Guid? user_id {  get; set; } // Người tạo

    public bool? is_personal { get; set; }
}