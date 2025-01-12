using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Entitites.RoomManangement.fee;
using RentRoomManagement.Common.Param;

namespace RentRoomManagement.BL.RoomManagement.FeeBL
{
    public interface IFeeBL : IBaseBL<FeeEntity, FeeDto>
    {
        Task GenerateFees(Guid buildingID);

        Task Pay(PaymentEntity payment);

        Task<PaymentDtoClient> GetPaymentInfo(PaymentParam paymentParam);

        Task<FeeDetailDtoClient> GetDetail(Guid recordID);
    }
}
