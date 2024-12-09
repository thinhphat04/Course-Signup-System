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

    // Lấy danh sách tất cả các khoản thanh toán học phí
    public async Task<List<TuitionFeePaymentDTO>> GetTuitionFeePayment()
    {
        // Lấy dữ liệu từ bảng TuitionFeePayments, bao gồm thông tin sinh viên và lớp học liên quan
        var TuitionFeePayment = await _courseSystemDB.TuitionFeePayments
                                    .Include(st => st.StudentClass.Student) // Bao gồm thông tin sinh viên
                                    .Include(st => st.StudentClass.Class) // Bao gồm thông tin lớp học
                                    .ToListAsync();

        // Ánh xạ danh sách các khoản thanh toán học phí từ entity sang DTO
        return _mapper.Map<List<TuitionFeePaymentDTO>>(TuitionFeePayment);
    }

    // Xử lý logic để thanh toán học phí
    public async Task<TuitionFeePaymentDTO> TuitionFeePayment(TuitionFeePaymentDTO TuitionFeePaymentDTO)
    {
        // Map DTO sang Entity để xử lý lưu vào cơ sở dữ liệu
        var TuitionFeePayment = _mapper.Map<TuitionFeePayment>(TuitionFeePaymentDTO);
        TuitionFeePayment.CreateAt = DateTime.Now; // Gán ngày thanh toán là ngày hiện tại

        _courseSystemDB.TuitionFeePayments.Add(TuitionFeePayment); // Thêm khoản thanh toán vào database
        await _courseSystemDB.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

        // Lấy thông tin lớp học từ StudentClass và cập nhật trạng thái lớp học
        var studentClass = await _courseSystemDB.StudentClasses.FindAsync(TuitionFeePaymentDTO.StudentClassId);
        studentClass!.Status = true; // Đánh dấu trạng thái lớp học là đã thanh toán
        await _courseSystemDB.SaveChangesAsync(); // Lưu thay đổi trạng thái

        // Map Entity đã cập nhật thành DTO để trả về
        return _mapper.Map<TuitionFeePaymentDTO>(TuitionFeePayment);
    }
}
}
