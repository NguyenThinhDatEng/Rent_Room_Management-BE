using System.Collections;

namespace RentRoomManagement.Common.Query
{
    public class PagingResult
    {
        public IEnumerable? Data { get; set; }

        public int TotalCount { get; set; }
    }
}
