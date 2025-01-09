using RentRoomManagement.Common.Entities.Dictionary;
using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Entitites.RoomManangement;
using RentRoomManagement.Common.Query;
using RentRoomManagement.DL.RoomManagement;

namespace RentRoomManagement.BL.RoomManagement
{
    public class HouseholdBL : BaseBL<HouseholdEntity, HouseholdDto>, IHouseholdBL
    {
        private IHouseholdDL _houseHoldDL;
        public HouseholdBL(IHouseholdDL houseHoldDL) : base(houseHoldDL)
        {
            _houseHoldDL = houseHoldDL;
        }

        /// <summary>
        /// Lấy danh sách hộ gia đình
        /// </summary>
        public override async Task<PagingResult> GetPaging(DictionaryPagingItem pagingItem)
        {
            if (pagingItem.Filters?.Count > 0)
            {
                var item = pagingItem.Filters.Find(x => x.Field == nameof(BuildingEntity.building_id));

                var value = item?.Value;
                if (value != null && value is Guid && (Guid)value != Guid.Empty)
                {
                    await _houseHoldDL.CreateHouseholdList((Guid)value);
                }

            }
            return await base.GetPaging(pagingItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public async Task<HouseholdDetail> GetDetail(Guid roomID)
        {
            return await _houseHoldDL.GetDetail(roomID);
        }
    }
}