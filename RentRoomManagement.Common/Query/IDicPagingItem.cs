namespace RentRoomManagement.Common.Query
{
    public interface IDicPagingItem: IPagingItem
    {
        SearchItem? SearchItem { get; set; }

        bool IsOr { get; set; }

        List<FilterItem> OrGroup { get; set; }
    }
}
