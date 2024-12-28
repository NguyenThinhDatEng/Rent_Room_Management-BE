using RentRoomManagement.Common.Query;

namespace RentRoomManagement.Common.Param
{
    public class RoomFilterParam
    {
        public DictionaryPagingItem? PagingItem { get; set; }
        
        public List<int>? FilterVals { get; set; }
    }
}
