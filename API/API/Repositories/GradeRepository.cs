using AutoMapper;
using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace API.Repositories
{
   public class GradeRepository : IGradeService
{
    private readonly CourseSystemDB _db; 
    private readonly IMapper _mapper;

    public GradeRepository(CourseSystemDB db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    // Tạo một grade mới và trả về DTO của grade đã tạo
    public async Task<GradeDTO> CreateGrade(GradeDTO GradeDTO)
    {
        var grade = _mapper.Map<Grade>(GradeDTO); // Map DTO thành entity Grade
        _db.Grades.Add(grade); // Thêm grade vào database
        await _db.SaveChangesAsync(); // Lưu thay đổi
        return _mapper.Map<GradeDTO>(grade); // Trả về grade đã tạo dưới dạng DTO
    }

    // Xóa grade theo ID và trả về thông báo thành công hoặc lỗi
    public async Task<ServiceResponse> DeleteGradeType(int GradeId)
    {
        var grade = await _db.Grades.FindAsync(GradeId); // Tìm grade theo ID
        if (grade == null)
        {
            return new ServiceResponse(false, "Grade is null"); // Nếu không tìm thấy, trả về lỗi
        }
        _db.Grades.Remove(grade); // Xóa grade khỏi database
        await _db.SaveChangesAsync(); // Lưu thay đổi
        return new ServiceResponse(true, "Delete success"); // Trả về thông báo thành công
    }

    // Lấy danh sách tất cả các grade, bao gồm các grade column liên quan
    public async Task<List<GradeDTO>> GetGrade()
    {
        var grades = await _db.Grades.Include(g => g.GradeColumn).ToListAsync(); // Lấy danh sách grade và bao gồm grade column

        // Map sang DTO và tính điểm trung bình nếu có grade column
        var gradeDTO = grades.Select(g => new GradeDTO
        {
            GradeId = g.GradeId,
            SubjectId = g.SubjectId,
            GradeTypeId = g.GradeTypeId,
            UserId = g.UserId,
            AverageScore = g.GradeColumn.Any() ? g.GradeColumn.Average(g => g.Score) : 0
        }).ToList();

        return gradeDTO!;
    }

    // Lấy thông tin của một grade cụ thể theo ID, bao gồm các grade column liên quan
    public async Task<GradeDTO> GetGradeById(int GradeId)
    {
        var grade = await _db.Grades.Include(g => g.GradeColumn).FirstOrDefaultAsync(g => g.GradeId == GradeId); // Tìm grade theo ID và bao gồm grade column
        if (grade == null)
        {
            throw new ArgumentNullException("Grade is null"); // Ném lỗi nếu không tìm thấy grade
        }

        // Map grade thành DTO và tính điểm trung bình nếu có grade column
        var gradeDTO = new GradeDTO
        {
            GradeId = grade.GradeId,
            SubjectId = grade.SubjectId,
            GradeTypeId = grade.GradeTypeId,
            UserId = grade.UserId,
            AverageScore = grade.GradeColumn.Any() ? grade.GradeColumn.Average(g => g.Score) : 0
        };
        return gradeDTO;
    }

    // Cập nhật một grade theo ID và trả về thông báo thành công hoặc lỗi
    public async Task<ServiceResponse> UpdateGrade(int Id, GradeDTO GradeDTO)
    {
        var grade = await _db.Grades.FindAsync(Id); // Tìm grade theo ID
        if (grade == null)
        {
            return new ServiceResponse(false, "Grade is null"); // Nếu không tìm thấy, trả về lỗi
        }

        // Cập nhật thông tin grade
        grade.GradeTypeId = GradeDTO.GradeTypeId;
        grade.SubjectId = GradeDTO.SubjectId;
        await _db.SaveChangesAsync(); // Lưu thay đổi
        return new ServiceResponse(true, "Update success"); // Trả về thông báo thành công
    }

    // Lấy bảng điểm tổng hợp cho tất cả sinh viên, bao gồm các grade type và grade column liên quan
    public async Task<List<AcademicTranscript>> GetAcademicTranscript()
    {
        var Transcript = await _db.Grades
                               .Include(g => g.GradeType)
                               .Include(g => g.Subject)
                               .Include(g => g.Student)
                               .Include(g => g.GradeColumn)
                               .ToListAsync(); // Lấy tất cả grades kèm các thông tin liên quan

        // Nhóm dữ liệu theo sinh viên và môn học, tính điểm trung bình
        return Transcript.GroupBy(g => new { g.Student.UserId, g.Subject.SubjectName })
            .Select(group => new AcademicTranscript
            {
                StudentName = group.First().Student.LastName + " " + group.First().Student.FirstName, // Tên sinh viên
                SubjectName = group.Key.SubjectName, // Tên môn học
                GradeTypes = group.Select(g => g.GradeType).Where(gt => gt != null) // Danh sách loại điểm
                                    .Distinct().Select(gt => new GradeTypeDTO
                                    {
                                        GradeTypeId = gt!.GradeTypeId,
                                        Coefficient = gt.Coefficient,
                                        GradeTypeName = gt.GradeTypeName
                                    }).ToList(),
                GradeColumns = group.SelectMany(g => g.GradeColumn) // Danh sách điểm từng cột
                            .Distinct()
                            .Select(gc => new GradeColumnDTO
                            {
                                Id = gc.Id,
                                GradeId = gc.GradeId,
                                Score = gc.Score
                            }).ToList(),
                SubjectGradePointAverage = group.SelectMany(g => g.GradeColumn).Any() // Điểm trung bình môn
                    ? group.SelectMany(g => g.GradeColumn).GroupBy(gc => gc.Grade.GradeTypeId)
                   .Sum(subGroup => subGroup.Sum(gc => gc.Score * group.First(g => g.GradeType.GradeTypeId == gc.Grade.GradeTypeId)
                                                .GradeType!.Coefficient)
                                            / group.SelectMany(g => g.GradeColumn).GroupBy(gc => gc.Grade.GradeTypeId)
                                        .Sum(subGroup =>
                                    subGroup.Sum(gc => group.First(g => g.GradeType.GradeTypeId == gc.Grade.GradeTypeId)
                                    .GradeType!.Coefficient)))
                    : 0
            }).ToList();
    }

    // Lấy bảng điểm chi tiết cho một sinh viên cụ thể
    public async Task<AcademicTranscript> GetAcademicTranscriptByStudent(string studentId)
    {
        var grades = await _db.Grades
                              .Include(g => g.GradeType)
                              .Include(g => g.Subject)
                              .Include(g => g.Student)
                              .Include(g => g.GradeColumn)
                              .Where(g => g.UserId == studentId) // Lọc theo ID sinh viên
                              .ToListAsync();

        if (grades is null)
        {
            throw new Exception("No grades found for the specified student."); // Không tìm thấy dữ liệu
        }

        // Tính toán điểm trung bình theo từng môn học
        return grades.GroupBy(g => new { g.Student.UserId, g.Subject.SubjectName })
            .Select(group => new AcademicTranscript
            {
                StudentName = group.First().Student.LastName + " " + group.First().Student.FirstName, // Tên sinh viên
                SubjectName = group.Key.SubjectName, // Tên môn học
                GradeTypes = group.Select(g => g.GradeType).Where(gt => gt != null) // Danh sách loại điểm
                                    .Distinct().Select(gt => new GradeTypeDTO
                                    {
                                        GradeTypeId = gt!.GradeTypeId,
                                        Coefficient = gt.Coefficient,
                                        GradeTypeName = gt.GradeTypeName
                                    }).ToList(),
                GradeColumns = group.SelectMany(g => g.GradeColumn) // Danh sách điểm từng cột
                            .Distinct()
                            .Select(gc => new GradeColumnDTO
                            {
                                Id = gc.Id,
                                GradeId = gc.GradeId,
                                Score = gc.Score
                            }).ToList(),
                SubjectGradePointAverage = group.SelectMany(g => g.GradeColumn).Any() // Điểm trung bình môn
                    ? group.SelectMany(g => g.GradeColumn).GroupBy(gc => gc.Grade.GradeTypeId)
                   .Sum(subGroup => subGroup.Sum(gc => gc.Score * group.First(g => g.GradeType.GradeTypeId == gc.Grade.GradeTypeId)
                                                .GradeType!.Coefficient)
                                            / group.SelectMany(g => g.GradeColumn).GroupBy(gc => gc.Grade.GradeTypeId)
                                        .Sum(subGroup =>
                                    subGroup.Sum(gc => group.First(g => g.GradeType.GradeTypeId == gc.Grade.GradeTypeId)
                                    .GradeType!.Coefficient)))
                    : 0
            }).FirstOrDefault()!;
    }

    // Lấy điểm cụ thể theo loại điểm và ID sinh viên
    public async Task<GradeDTO> GetGradeByGradeType(int GradeTypeId, string studentId)
    {
        var grade = await _db.Grades
                             .Include(g => g.GradeType)
                             .Include(g => g.Subject)
                             .Include(g => g.Student)
                             .Include(g => g.GradeColumn)
                             .FirstOrDefaultAsync(g => g.UserId == studentId && g.GradeTypeId == GradeTypeId); // Lọc theo loại điểm và sinh viên
        if (grade == null)
        {
            throw new Exception("Student doesn't have a grade."); // Không tìm thấy dữ liệu
        }

        // Map grade thành DTO và tính điểm trung bình
        return new GradeDTO
        {
            GradeId = grade.GradeId,
            SubjectId = grade.SubjectId,
            GradeTypeId = grade.GradeTypeId,
            UserId = grade.UserId,
            AverageScore = grade.GradeColumn.Any() ? grade.GradeColumn.Average(g => g.Score) : 0
        };
    }
}
}