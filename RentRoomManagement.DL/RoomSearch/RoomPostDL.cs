using MySqlConnector;
using RentRoomManagement.Common.Entitites.RoomSearch.RoomPost;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Param;
using RentRoomManagement.Common.Query;

namespace RentRoomManagement.DL.RoomSearch
{
    public class RoomPostDL : BaseDL<RoomPostDtoEdit, RoomPostDtoClient>, IRoomPostDL
    {
        /// <summary>
        /// Yêu thích/Hủy yêu thích bài đăng
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Guid?> LovePost(FavoritePostParam param)
        {
            var affectRows = 0;
            if (param.favorite_post_id == null)
            {
                var favoriteItem = new FavoritePostEntity()
                {
                    favorite_post_id = Guid.NewGuid(),
                    room_post_id = param.room_post_id ?? Guid.Empty,
                    user_id = param.user_id ?? Guid.Empty,
                };
                var newItem = await InsertAsync(favoriteItem);
                if (newItem != null)
                {
                    return newItem.favorite_post_id;
                }
            }
            else
            {
                affectRows = DeleteFavoritePost(param);
            }

            return null;
        }

        /// <summary>
        /// API Xóa 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID bản ghi cần xóa</param>
        /// <returns>ID bản ghi được xóa</return
        /// <author>NVThinh 10/1/2023</author>
        public int DeleteFavoritePost(FavoritePostParam param)
        {
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                mySqlConnection.Open();

                // Thực hiện chèn dữ liệu vào bảng
                var tableName = TableNameMapper<FavoritePostEntity>();
                MySqlCommand command = new MySqlCommand($"DELETE FROM {tableName} WHERE " +
                    $"{nameof(FavoritePostEntity.user_id)} = @userId AND " +
                    $"{nameof(FavoritePostEntity.room_post_id)} = @roomPostId", mySqlConnection);

                command.Parameters.AddWithValue("@userId", param.user_id);
                command.Parameters.AddWithValue("@roomPostId", param.room_post_id);

                // Thực thi câu truy vấn
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FilterVals"></param>
        /// <returns></returns>
        public async Task<List<Guid>> FilterByCharacteristic(List<int> filterVals)
        {
            var condition = new PagingItem()
            {
                Filters = new List<FilterItem>()
                {
                    new FilterItem()
                    {
                        Field = nameof(RoomFilterEntity.filter_value),
                        Operator = FilterOperator.IN,
                        Value = string.Join(",", filterVals)
                    }
                }
            };
            var items = await GetAll<RoomFilterEntity>(condition);
            return items.Select(x => x.room_post_id).ToList();
        }

        /// <summary>
        /// Lấy các bài viết đã yêu thích
        /// </summary>
        public async Task<List<RoomPostDtoClient>> GetFavoritePosts(Guid? userID)
        {
            var result = new List<RoomPostDtoClient>();
            if (userID == null)
            {
                return result;
            }

            var pagingItem = new PagingItem();

            pagingItem.Columns.Add(nameof(FavoritePostEntity.room_post_id));
            pagingItem.Filters = new List<FilterItem>()
            {
                new FilterItem()
                {
                    Field = nameof(FavoritePostEntity.user_id),
                    Operator = FilterOperator.Equal,
                    Value = userID
                }
            };

            var favoriteRecords = await GetAll<FavoritePostEntity>(pagingItem);
            if (favoriteRecords != null && favoriteRecords.Any())
            {
                var postIDs = favoriteRecords.Select(x => x.room_post_id).ToList();

                pagingItem = new PagingItem();
                pagingItem.Columns = new List<string>()
                {
                    nameof(RoomPostEntity.room_post_id),
                    nameof(RoomPostEntity.room_address),
                    nameof(RoomPostEntity.room_price),
                    nameof(RoomPostEntity.room_description),
                    nameof(RoomPostEntity.room_area),
                    nameof(RoomPostEntity.posted_date),
                    nameof(RoomPostEntity.post_title),
                    nameof(RoomPostDtoClient.favorite_post_id)
                };

                pagingItem.Filters.Add(new FilterItem()
                {
                    Field = nameof(RoomPostEntity.room_post_id),
                    Operator = FilterOperator.IN,
                    Value = postIDs
                });

                pagingItem.Sorts = new List<SortItem>()
                {
                    new SortItem()
                    {
                        Column = nameof(RoomPostEntity.posted_date),
                        IsAscending = false
                    }
                };

                result = await GetAll<RoomPostDtoClient>(pagingItem) as List<RoomPostDtoClient>;
            }

            return result;
        }
    }
}