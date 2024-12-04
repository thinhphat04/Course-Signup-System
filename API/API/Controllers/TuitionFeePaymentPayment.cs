using API.DTO;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class TuitionFeePaymentPayment : ControllerBase
    {
        private readonly ITuitionFeePaymentService _TuitionFeePaymentService;
        public TuitionFeePaymentPayment(ITuitionFeePaymentService paymentService)
        {
            _TuitionFeePaymentService = paymentService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateTuitionFeePayment(TuitionFeePaymentDTO TuitionFeePaymentDTO)
        {
            try
            {
                var TuitionFeePayment = await _TuitionFeePaymentService.TuitionFeePayment(TuitionFeePaymentDTO);
                return Ok(TuitionFeePayment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetTuitionFeePayment()
        {
            try
            {
                var TuitionFeePayment = await _TuitionFeePaymentService.GetTuitionFeePayment();
                return Ok(TuitionFeePayment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

