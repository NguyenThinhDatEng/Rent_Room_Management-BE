namespace RentRoomManagement.Common.Query
{
    public class DictionaryPagingItem : PagingItem, IDicPagingItem
    {
        public SearchItem? SearchItem { get; set; }

        public bool IsOr { get; set; } = true;

        public List<FilterItem> OrGroup { get; set; } = new List<FilterItem>();
    }
}
