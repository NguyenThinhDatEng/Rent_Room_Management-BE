using Newtonsoft.Json;
using RentRoomManagement.Common.Entitites.RoomSearch.RoomPost;
using RentRoomManagement.Common.Entitites.TDto;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Param;
using RentRoomManagement.Common.Query;
using RentRoomManagement.DL.RoomSearch;

namespace RentRoomManagement.BL.Tenant.RoomSearch
{
    public class RoomPostBL : BaseBL<RoomPostDtoEdit, RoomPostDtoClient>, IRoomPostBL
    {
        private IRoomPostDL _roomPostDL;

        public RoomPostBL(IRoomPostDL roomPostDL) : base(roomPostDL)
        {
            _roomPostDL = roomPostDL;
        }

        /// <summary>
        /// Lấy ra danh sách bài viết đã lưu/đăng
        /// </summary>
        public async Task<PagingResult> GetMyPosts(Guid? userID)
        {
            if (userID == null)
            {
                return new PagingResult();
            }

            var pagingItem = new DictionaryPagingItem()
            {
                Filters = new List<FilterItem>()
                {
                    new FilterItem()
                    {
                        Field = nameof(RoomPostDtoClient.user_id),
                        Value = userID,
                        Operator = FilterOperator.Equal
                    }
                }
            };
            return await GetPaging(pagingItem);
        }

        /// <summary>
        /// Lấy ra danh sách bài viết yêu thích
        /// </summary>
        public async Task<List<RoomPostDtoClient>> GetFavoritePosts(Guid? userID)
        {
            return await _roomPostDL.GetFavoritePosts(userID);
        }

        /// <summary>
        /// Yêu thích bài viết
        /// </summary>
        public async Task<object> LovePost(FavoritePostParam param)
        {
            try
            {
                var favoritePostID = await _roomPostDL.LovePost(param);
                return new ServiceResponse()
                {
                    Data = favoritePostID
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ErrorCode = ex.HResult
                };
            }
        }

        /// <summary>
        /// Sau khi cất thành công
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected override async Task AfterInsertSync(RoomPostDtoEdit entity)
        {
            if (entity.room_characteristic == null)
            {
                return;
            }

            var roomCharacteristic = JsonConvert.DeserializeObject<List<int>>(entity.room_characteristic);
            if (roomCharacteristic != null && roomCharacteristic.Any())
            {
                foreach (var item in roomCharacteristic)
                {
                    try
                    {
                        var roomFilter = new RoomFilterEntity()
                        {
                            room_post_id = entity.room_post_id,
                            filter_value = item
                        };

                        _ = await InsertAsync(roomFilter);
                    }
                    catch (Exception ex) { }
                }
            }

        }

        /// <summary>
        /// Custum bộ lọc
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<PagingResult> GetPagingCustom(RoomFilterParam param)
        {
            var pagingItem = param.PagingItem ?? new DictionaryPagingItem();

            var roomPostIDs = new List<Guid>();
            if (param.FilterVals != null && param.FilterVals.Count > 0)
            {
                roomPostIDs = await _roomPostDL.FilterByCharacteristic(param.FilterVals);
                if (roomPostIDs.Any())
                {
                    var filterList = pagingItem.Filters ?? new List<FilterItem>();
                    filterList.Add(new FilterItem()
                    {
                        Field = nameof(RoomPostEntity.room_post_id),
                        Operator = FilterOperator.IN,
                        Value = string.Join(",", roomPostIDs.Select(x => $"'{x}'"))
                    });

                    // Lấy lại bộ lọc
                    pagingItem.Filters = filterList;
                } else
                {
                    return new PagingResult()
                    {
                        Data = new List<RoomPostDtoClient>(),
                        TotalCount = 0,
                    };
                }
            }

            // Lấy dữ liệu
            var pagingResult = await base.GetPaging(pagingItem);
            return pagingResult;
        }
    }
}
