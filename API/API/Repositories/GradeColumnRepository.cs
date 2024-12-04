using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class GradeColumnRepository : IGradeColumnService
    {
        private readonly IMapper _mapper;
        private readonly CourseSystemDB _courseSystemDB;
        public GradeColumnRepository(IMapper mapper, CourseSystemDB courseSystemDB)
        {
            _mapper = mapper;
            _courseSystemDB = courseSystemDB;
        }

        public async Task<GradeColumnDTO> CreateGrade(GradeColumnDTO GradeColumnDTO)
        {
            
            var grade = _mapper.Map<GradeColumn>(GradeColumnDTO); 
            var subjectGrade = await _courseSystemDB.SubjectGradeTypes.FirstOrDefaultAsync(g => g.SubjectId == grade.Grade.SubjectId && g.GradeTypeId == grade.Grade.GradeTypeId);
            var gradeColumn = await _courseSystemDB.GradeColumns.Where(f => f.GradeId == GradeColumnDTO.GradeId).CountAsync();

            if (subjectGrade == null)
            {
                throw new Exception("subjectGrade is null");
            }
            else if (subjectGrade.MandatoryColumnGrade > gradeColumn || subjectGrade.GradeColumn > gradeColumn)
            {
                _courseSystemDB.GradeColumns.Add(grade);
                await _courseSystemDB.SaveChangesAsync();
            }
            else 
            {
                throw new Exception("the spot has darkened");
            }
            return _mapper.Map<GradeColumnDTO>(grade);
        }

        public async Task<ServiceResponse> DeleteGradeType(int GradeColumnId)
        {
            var grade = await _courseSystemDB.GradeColumns.FindAsync(GradeColumnId);
            if(grade == null)
            {
                return new ServiceResponse(false, "grade column is null");
            }
            _courseSystemDB.GradeColumns.Remove(grade);
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "delete success");
        }

        public async Task<List<GradeColumnDTO>> GetGrade()
        {
            var grade = await _courseSystemDB.GradeColumns.ToListAsync();
            return _mapper.Map<List<GradeColumnDTO>>(grade);
        }

        public async Task<GradeColumnDTO> GetGradeById(int GradeColumnId)
        {
            var grade = await _courseSystemDB.GradeColumns.FindAsync(GradeColumnId);
            if (grade == null)
            {
                throw new ArgumentNullException( "grade column is null");
            };
            return _mapper.Map<GradeColumnDTO>(grade);
        }

        public async Task<ServiceResponse> UpdateGrade(int Id, GradeColumnDTO GradeColumnDTO)
        {
            var grade = await _courseSystemDB.GradeColumns.FindAsync(Id);
            if (grade == null)
            {
                return new ServiceResponse(false, "grade column is null");
            }
            grade.Score = GradeColumnDTO.Score;
            grade.GradeId = GradeColumnDTO.GradeId;
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "update success");
        }
    }
}
