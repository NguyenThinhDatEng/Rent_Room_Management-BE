using RentRoomManagement.Common.Entitites.DTO;
using RentRoomManagement.Common.Entitites.RoomManangement.fee;
using RentRoomManagement.Common.Param;
using RentRoomManagement.DL.RoomManagement.FeeDL;

namespace RentRoomManagement.BL.RoomManagement.FeeBL
{
    public class FeeBL : BaseBL<FeeEntity, FeeDto>, IFeeBL
    {
        private IFeeDL _feeDL;
        public FeeBL(IFeeDL feeDL) : base(feeDL)
        {
            _feeDL = feeDL;
        }

        public async Task GenerateFees(Guid buildingID)
        {
            await _feeDL.GenerateFees(buildingID);
        }

        /// <summary>
        /// Thanh toán
        /// </summary>
        public async Task Pay(PaymentEntity payment)
        {
            _ = await InsertAsync(payment);

            var feeItem = await GetByID(payment.fee_id);
            if (feeItem != null)
            {
                feeItem.received_fee += payment.payment_amount;
                feeItem.updated_date = DateTime.Now;

                if (feeItem.received_fee >= feeItem.total_fee)
                {
                    feeItem.fee_status = 2;
                }
                else if (feeItem.received_fee > 0)
                {
                    feeItem.fee_status = 1;
                }
                else
                {
                    feeItem.fee_status = 0;
                }

                UpdateAsync(feeItem);
            }
        }

        public async Task<PaymentDtoClient> GetPaymentInfo(PaymentParam paymentParam)
        {
            return await _feeDL.GetPaymentInfo(paymentParam);
        }

        public async Task<FeeDetailDtoClient> GetDetail(Guid recordID)
        {
            return await _feeDL.GetDetail(recordID);
        }
    }
}