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

        public async Task<GradeDTO> CreateGrade(GradeDTO GradeDTO)
        {
            var grade = _mapper.Map<Grade>(GradeDTO);
                    _db.Grades.Add(grade);
            await _db.SaveChangesAsync();
            return _mapper.Map<GradeDTO>(grade);
        }

        public async Task<ServiceResponse> DeleteGradeType(int GradeId)
        {
            var grade = await _db.Grades.FindAsync(GradeId);
            if (grade == null)
            {
                return new ServiceResponse(false, "Grade is null ");
            }
            _db.Grades.Remove(grade);
            await _db.SaveChangesAsync();
            return new ServiceResponse(true, "delete success");
        }
        public async Task<List<GradeDTO>> GetGrade()
        {
            var grades = await _db.Grades.Include(g => g.GradeColumn).ToListAsync();

            var gradeDTO = grades.Select(g => new GradeDTO
            {
                GradeId = g.GradeId,
                SubjectId = g.SubjectId,
                GradeTypeId = g.GradeTypeId,
                UserId = g.UserId,
                AverageScore = g.GradeColumn.Any() ? g.GradeColumn.Average(g => g. Score) : 0
            }).ToList();
            return gradeDTO!;
        }

        public async Task<GradeDTO> GetGradeById(int GradeId)
        {
            var grade = await _db.Grades.Include(g => g.GradeColumn).FirstOrDefaultAsync(g =>g.GradeId == GradeId);
            if (grade == null)
            {
               throw new ArgumentNullException( "Grade is null");
            }
            var gradeDTO =  new GradeDTO
            {
                GradeId = grade.GradeId,
                SubjectId = grade.SubjectId,
                GradeTypeId = grade.GradeTypeId,
                UserId = grade.UserId,
                AverageScore = grade.GradeColumn.Any() ? grade.GradeColumn.Average(g => g.Score)  : 0
            };
            return gradeDTO;
        }

        public async Task<ServiceResponse> UpdateGrade(int Id, GradeDTO GradeDTO)
        {
            var grade = await _db.Grades.FindAsync(Id);
            if (grade == null)
            {
                return new ServiceResponse(false, "Grade is null ");
            }
            grade.GradeTypeId = GradeDTO.GradeTypeId;
            grade.SubjectId = GradeDTO.SubjectId;
            grade.SubjectId = GradeDTO.SubjectId;
            await _db.SaveChangesAsync();
            return new ServiceResponse(true, "update success");
        }

        public async Task<List<AcademicTranscript>> GetAcademicTranscript()
        {
            var Transcript = await _db.Grades
                               .Include(g => g.GradeType)
                               .Include(g => g.Subject)
                               .Include(g => g.Student)
                               .Include(g => g.GradeColumn)
                               .ToListAsync();

            return Transcript.GroupBy(g => new {g.Student.UserId, g.Subject.SubjectName})
                .Select(group => new AcademicTranscript
                {
                    StudentName = group.First().Student.LastName + " " + group.First().Student.FirstName,
                    SubjectName = group.Key.SubjectName,
                    GradeTypes = group.Select(g => g.GradeType).Where(gt => gt != null)
                                            .Distinct().Select(gt => new GradeTypeDTO
                                            {
                                                GradeTypeId = gt!.GradeTypeId,
                                                Coefficient = gt.Coefficient,
                                                GradeTypeName = gt.GradeTypeName
                                            }).ToList(),
                    GradeColumns = group.SelectMany(g => g.GradeColumn)
                                .Distinct()
                                .Select(gc => new GradeColumnDTO
                                {
                                    Id = gc.Id,
                                    GradeId = gc.GradeId,
                                    Score = gc.Score
                                }).ToList(),
                    SubjectGradePointAverage = group.SelectMany(g => g.GradeColumn).Any()
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

        public async Task<AcademicTranscript> GetAcademicTranscriptByStudent(string studentId)
        {
            var grades = await _db.Grades
                                  .Include(g => g.GradeType)
                                  .Include(g => g.Subject)
                                  .Include(g => g.Student)
                                  .Include(g => g.GradeColumn)
                                  .Where(g => g.UserId == studentId) // Lọc theo StudentId và ClassId
                                  .ToListAsync();

            if (grades is null)
            {
                throw new Exception("No grades found for the specified student and class.");
            }
            return grades.GroupBy(g => new { g.Student.UserId, g.Subject.SubjectName })
       .Select(group => new AcademicTranscript
       {
           StudentName = group.First().Student.LastName + " " + group.First().Student.FirstName,
           SubjectName = group.Key.SubjectName,
           GradeTypes = group.Select(g => g.GradeType).Where(gt => gt != null)
                                   .Distinct().Select(gt => new GradeTypeDTO
                                   {
                                       GradeTypeId = gt!.GradeTypeId,
                                       Coefficient = gt.Coefficient,
                                       GradeTypeName = gt.GradeTypeName
                                   }).ToList(),
           GradeColumns = group.SelectMany(g => g.GradeColumn)
                       .Distinct()
                       .Select(gc => new GradeColumnDTO
                       {
                           Id = gc.Id,
                           GradeId = gc.GradeId,
                           Score = gc.Score
                       }).ToList(),
           SubjectGradePointAverage = group.SelectMany(g => g.GradeColumn).Any()
               ? group.SelectMany(g => g.GradeColumn).GroupBy(gc => gc.Grade.GradeTypeId)
              .Sum(subGroup => subGroup.Sum(gc => gc.Score * group.First(g => g.GradeType.GradeTypeId == gc.Grade.GradeTypeId)
                                           .GradeType!.Coefficient)
                                       / group.SelectMany(g => g.GradeColumn).GroupBy(gc => gc.Grade.GradeTypeId)
                                   .Sum(subGroup =>
                               subGroup.Sum(gc => group.First(g => g.GradeType.GradeTypeId == gc.Grade.GradeTypeId)
                               .GradeType!.Coefficient)))
       : 0
       }).FirstOrDefault()!; // Chỉ lấy phần tử đầu tiên

        }

        public async Task<GradeDTO> GetGradeByGradeType(int GradeTypeId, string studentId)
        {
            var grade = await _db.Grades
                                 .Include(g => g.GradeType)
                                 .Include(g => g.Subject)
                                 .Include(g => g.Student)
                                 .Include(g => g.GradeColumn)
                                 .FirstOrDefaultAsync(g => g.UserId == studentId && g.GradeTypeId == GradeTypeId);
            if(grade == null)
            {
                throw new Exception("student don't grade");
            }
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

