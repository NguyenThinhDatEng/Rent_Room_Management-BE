using Dapper;
using MySqlConnector;
using RentRoomManagement.Common.Entitites.Dictionary;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Entitites.RoomManangement.fee;
using RentRoomManagement.Common.Enums;
using RentRoomManagement.Common.Param;
using RentRoomManagement.Common.Query;

namespace RentRoomManagement.DL.RoomManagement.FeeDL
{
    public class FeeDL : BaseDL<FeeEntity, FeeDto>, IFeeDL
    {
        /// <summary>
        /// Sinh thu phí
        /// </summary>
        public async Task GenerateFees(Guid buildingID)
        {
            MySqlConnection connection = new MySqlConnection(DatabaseContext.ConnectionString);
            await connection.OpenAsync();

            MySqlCommand command = new MySqlCommand("CALL Proc_Generate_Fee(@buildingID)", connection);

            command.Parameters.AddWithValue("@buildingID", buildingID);

            _ = await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task<FeeDetailDtoClient> GetDetail(Guid recordID)
        {
            var result = new FeeDetailDtoClient();

            MySqlConnection connection = new MySqlConnection(DatabaseContext.ConnectionString);
            await connection.OpenAsync();

            var sql = "CALL Proc_GetFeeDetail(@feeID)";

            var param = new Dictionary<string, object>()
            {
                {"feeID", recordID }
            };

            var data = (await connection.QueryAsync<VehicleFeeDetail>(sql, param)) as List<VehicleFeeDetail>;
            if (data?.Count > 0)
            {
                var owner = data.Find(x => x.is_owner == true);
                if (owner != null)
                {
                    result.resident_name = owner.resident_name;
                };

                result.member_count = data.Select(x => x.resident_id).Distinct().Count();

                var firstItem = data[0];
                result.room_area = firstItem.room_area;
                result.room_name = firstItem.room_name;
                result.room_price = firstItem.room_price;

                var vehicleList = data.Where(x => x.vehicle_id != null).ToList();
                if (vehicleList.Count > 0)
                {
                    result.vehicles = MapList<VehicleFeeDetail, VehicleDto>(vehicleList);
                }
            }

            await connection.CloseAsync();

            return result;
        }

        public static List<TDestination> MapList<TSource, TDestination>(List<TSource> sourceList)
        where TSource : class
        where TDestination : class, new()
        {
            List<TDestination> destinationList = new List<TDestination>();

            foreach (var source in sourceList)
            {
                var destination = new TDestination();

                foreach (var sourceProp in typeof(TSource).GetProperties())
                {
                    var destProp = typeof(TDestination).GetProperty(sourceProp.Name);
                    if (destProp != null && destProp.CanWrite)
                    {
                        destProp.SetValue(destination, sourceProp.GetValue(source));
                    }
                }

                destinationList.Add(destination);
            }

            return destinationList;
        }

        public async Task<PaymentDtoClient> GetPaymentInfo(PaymentParam paymentParam)
        {
            var result = new PaymentDtoClient();

            MySqlConnection connection = new MySqlConnection(DatabaseContext.ConnectionString);
            await connection.OpenAsync();

            var paging = new PagingItem();

            paging.Filters.Add(new FilterItem()
            {
                Field = nameof(ResidentEntity.room_id),
                Value = paymentParam.RoomID,
                Operator = FilterOperator.Equal
            });

            paging.Filters.Add(new FilterItem()
            {
                Field = nameof(ResidentEntity.is_owner),
                Value = 1,
                Operator = FilterOperator.Equal
            });

            var owner = (await GetAll<ResidentEntity>(paging)).FirstOrDefault();
            if (owner != null)
            {
                result.resident_code = owner.resident_code;
                result.resident_name = owner.resident_name;
            }

            paging = new PagingItem();

            paging.Filters.Add(new FilterItem()
            {
                Field = nameof(PaymentEntity.fee_id),
                Value = paymentParam.FeeID,
                Operator = FilterOperator.Equal
            });

            var paymentList = await GetAll<PaymentEntity>(paging);
            result.payments = paymentList;

            await connection.CloseAsync();

            return result;
        }
    }
}