namespace RentRoomManagement.Common.Query
{
    public class PagingItem : IPagingItem
    {
        public int Skip { get; set; } = 0;

        public int? Take { get; set; }

        public List<string> Columns { get; set; } = new List<string>();

        public List<FilterItem> Filters { get; set; } = new List<FilterItem>();

        public List<SortItem> Sorts { get; set; } = new List<SortItem>();
    }
}
