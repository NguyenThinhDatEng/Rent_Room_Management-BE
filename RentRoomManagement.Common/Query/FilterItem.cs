using RentRoomManagement.Common.Enums;

namespace RentRoomManagement.Common.Query
{
    public class FilterItem
    {
        public string Field { get; set; }
        public object Value { get; set; }
        public FilterOperator Operator { get; set; }
    }

}
