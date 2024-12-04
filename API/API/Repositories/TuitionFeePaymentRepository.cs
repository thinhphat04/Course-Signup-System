using AutoMapper;

using API.DTO;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class TuitionFeePaymentRepository : ITuitionFeePaymentService
    {
        private readonly IMapper _mapper;
        private readonly CourseSystemDB _courseSystemDB;
        public TuitionFeePaymentRepository(IMapper mapper, CourseSystemDB courseSystemDB)
        {
            _mapper = mapper;
            _courseSystemDB = courseSystemDB;
        }

        public async Task<List<TuitionFeePaymentDTO>> GetTuitionFeePayment()
        {
            var TuitionFeePayment = await _courseSystemDB.TuitionFeePayments.Include(st => st.StudentClass.Student)
                                                              .Include(st=> st.StudentClass.Class)
                                                              .ToListAsync();
            return _mapper.Map<List<TuitionFeePaymentDTO>>(TuitionFeePayment);
        }

        public async Task<TuitionFeePaymentDTO> TuitionFeePayment(TuitionFeePaymentDTO TuitionFeePaymentDTO)
        {
           var TuitionFeePayment  = _mapper.Map<TuitionFeePayment>(TuitionFeePaymentDTO);
            TuitionFeePayment.CreateAt = DateTime.Now;
           _courseSystemDB.TuitionFeePayments.Add(TuitionFeePayment);
            await _courseSystemDB.SaveChangesAsync();
            var studentClass = await _courseSystemDB.StudentClasses.FindAsync(TuitionFeePaymentDTO.StudentClassId);
            studentClass!.Status = true;
            await _courseSystemDB.SaveChangesAsync();
            return _mapper.Map<TuitionFeePaymentDTO>(TuitionFeePayment);
        }
    }
}
