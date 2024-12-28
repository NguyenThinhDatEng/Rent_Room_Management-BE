using Dapper;
using MySqlConnector;
using RentRoomManagement.Common.Entitites.Location;
using RentRoomManagement.Common.Functions;

namespace RentRoomManagement.DL.Location
{
    public class LocationDL
    {
        public async Task<IEnumerable<LocationDto>> GetAllLocations()
        {
            var connection = new MySqlConnection(DatabaseContext.ConnectionString);
            await connection.OpenAsync();

            var tableName = BuildQuery.TableNameMapper<LocationDto>();
            var sql = $"SELECT * FROM {tableName}";

            var result = await connection.QueryAsync<LocationDto>(sql);
            return result;
        }
    }
}
