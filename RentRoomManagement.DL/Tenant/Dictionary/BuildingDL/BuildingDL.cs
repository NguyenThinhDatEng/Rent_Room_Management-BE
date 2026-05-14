using Dapper;
using MySqlConnector;
using RentRoomManagement.Common.Entities.Dictionary;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Functions;

namespace RentRoomManagement.DL.Tenant.Dictionary.BuildingDL
{
    public class BuildingDL : BaseDL<BuildingEntity, BuildingDto>, IBuildingDL
    {
        public async Task<bool> SetActiveBuilding(Guid buildingId, Guid userId)
        {
            using var connection = new MySqlConnection(DatabaseContext.ConnectionString);
            connection.Open();

            var table = BuildQuery.TableNameMapper<BuildingEntity>();
            var sql = $"UPDATE {table} SET " +
                $"{nameof(BuildingEntity.status)} = CASE " +
                $"WHEN {nameof(BuildingEntity.building_id)} = @buildingId THEN {(int)BuildingStatus.Using} " +
                $"ELSE {(int)BuildingStatus.NoUsing} END " +
                $"WHERE {nameof(BuildingEntity.user_id)} = @userId";

            var rows = await connection.ExecuteAsync(sql, new { buildingId, userId });
            return rows > 0;
        }
    }
}