using API.DTO;
using API.Entities;

namespace API.Services
{
    public interface ITuitionFeePaymentService
    {
        Task<List<TuitionFeePaymentDTO>> GetTuitionFeePayment();
        Task<TuitionFeePaymentDTO> TuitionFeePayment(TuitionFeePaymentDTO TuitionFeePaymentDTO);
    }
}
