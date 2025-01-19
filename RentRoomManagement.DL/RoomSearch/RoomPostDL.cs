using Dapper;
using MySqlConnector;
using RentRoomManagement.Common.Entities.Dictionary;
using RentRoomManagement.Common.Entitites;
using RentRoomManagement.Common.Entitites.Dictionary.Room;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Entitites.RoomSearch.RoomPost;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Functions;
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
                var tableName = BuildQuery.TableNameMapper<FavoritePostEntity>();
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

        public async Task<LinkingAccountEntity> LinkToInnkeeper(LinkingParam linkingParam)
        {
            using (var connection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                connection.Open();

                var userTable = BuildQuery.TableNameMapper<UserEntity>();

                var userIdField = nameof(UserEntity.user_id);
                var accountField = nameof(UserEntity.account);
                var innkeeperId = nameof(LinkingAccountEntity.innkeeper_id);

                string sql = $"SELECT u.{userIdField} as {innkeeperId} " +
                    $"FROM {userTable} u " +
                    $"JOIN user_roles ur ON ur.{userIdField} = u.{userIdField} AND ur.role_id = {(int)Role.Inkeeper} " +
                    $"WHERE u.{accountField} = @phoneNumber";

                var param = new Dictionary<string, object>()
                {
                    {"phoneNumber", linkingParam.PhoneNumber ?? "" },
                };

                var result = await connection.QueryFirstOrDefaultAsync<LinkingAccountEntity>(sql, param);
                if (result != null)
                {
                    return result;
                }

            }
            return default;
        }

        public async void DeleteLinking(Guid roomSeekerId)
        {
            using (var connection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                connection.Open();

               var table = BuildQuery.TableNameMapper<LinkingAccountEntity>();
                var idField = nameof(LinkingAccountEntity.room_seeker_id);

                var sql = $"DELETE FROM {table} WHERE {idField} = @id";

                MySqlCommand command = new MySqlCommand(sql, connection);

                command.Parameters.AddWithValue("@id", roomSeekerId);

                command.ExecuteNonQuery();
            }
        }

        public async Task<RoomPostDtoClient> GetNew(RoomPostParam roomPostParam)
        {
            var connection = new MySqlConnection(DatabaseContext.ConnectionString);
            await connection.OpenAsync();

            var roomIdField = nameof(RoomEntity.room_id);
            var buildingIdField = nameof(RoomEntity.building_id);
            var roomAddressFiled = nameof(RoomPostEntity.room_address);
            var buildingAddressField = nameof(BuildingEntity.building_address);

            var roomTable = BuildQuery.TableNameMapper<RoomEntity>();
            var buildingTable = BuildQuery.TableNameMapper<BuildingDto>();
            var roomPostLocationTable = BuildQuery.TableNameMapper<RoomPostLocationEntity>();

            var sql = $"Select r.*, " +
                $"b.* " +
                $"b.{buildingAddressField} as {roomAddressFiled} " +
                $"from {roomTable} where r.{roomIdField} = @roomId " +
                $"join {buildingTable} b on b.{buildingIdField} = r.{buildingIdField} ";

            // Thực hiện chèn dữ liệu vào bảng

            var param = new Dictionary<string, object>()
            {
                {"roomId", roomPostParam.room_id }
            };
            var result = await connection.QueryFirstOrDefaultAsync<RoomPostDtoClient>(sql, param);

            await connection.CloseAsync();

            return result;
        }
    }
}