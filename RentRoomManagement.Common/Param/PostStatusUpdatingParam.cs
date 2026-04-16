namespace RentRoomManagement.Common.Param
{
    public class PostStatusUpdatingParam
    {
        public Guid? RoomPostId { get; set; }

        public int? PostStatus {  get; set; }

        public string? RejectMessage { get; set; }
    }
}
