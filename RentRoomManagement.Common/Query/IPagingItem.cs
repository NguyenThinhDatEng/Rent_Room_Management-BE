namespace RentRoomManagement.Common.Query
{
    public interface IPagingItem
    {
        int Skip { get; set; }

        int Take { get; set; }

        List<string> Columns { get; set; }

        List<FilterItem> Filters { get; set; }

        List<SortItem> Sorts { get; set; }
    }
}
