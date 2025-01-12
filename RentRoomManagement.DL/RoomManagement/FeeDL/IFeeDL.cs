using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Entitites.RoomManangement.fee;
using RentRoomManagement.Common.Param;

namespace RentRoomManagement.DL.RoomManagement.FeeDL
{
    public interface IFeeDL : IBaseDL<FeeEntity, FeeDto>
    {
        Task GenerateFees(Guid buildingID);

        Task<PaymentDtoClient> GetPaymentInfo(PaymentParam paymentParam);

        Task<FeeDetailDtoClient> GetDetail(Guid recordID);
    }
}
