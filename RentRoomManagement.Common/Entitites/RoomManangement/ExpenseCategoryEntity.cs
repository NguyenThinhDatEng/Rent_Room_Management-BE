using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentRoomManagement.Common.Entitites.RoomManangement
{
    [Table("rhm_expense_category")]
    public class ExpenseCategoryEntity
    {
        [Key]
        public Guid expense_category_id { get; set; }

        public string? expense_category_name { get; set; }

        public Guid? user_id { get; set; }
    }
}
