namespace RentRoomManagement.Common.Param
{
    public class ExpenseStatisticParam
    {
        public int Year { get; set; }

        public int Month { get; set; } = -1;

        public int Week { get; set; } = -1;

        public int Day { get; set; } = -1;

        public int Hour { get; set; } = -1;

        public Guid userId { get; set; }
    }
}
