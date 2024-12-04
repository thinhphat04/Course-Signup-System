using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class SubjectGradeTypeRepository : ISubjectGradeTypeService
    {
        private readonly IMapper _mapper;
        private readonly CourseSystemDB _courseSystemDB;
        public SubjectGradeTypeRepository(IMapper mapper, CourseSystemDB courseSystemDB)
        {
            _mapper = mapper;
            _courseSystemDB = courseSystemDB;
        }

        public async Task<SubjectGradeTypeDTO> CreateSubjectGradeType(SubjectGradeTypeDTO SubjectGradeTypeDTO)
        {
            var grade = _mapper.Map<SubjectGradeType>(SubjectGradeTypeDTO);
            _courseSystemDB.SubjectGradeTypes.Add(grade);
            await _courseSystemDB.SaveChangesAsync();
            return _mapper.Map<SubjectGradeTypeDTO>(grade);
        }

        public async Task<ServiceResponse> DeleteSubjectGradeTypeType(int Id)
        {
           var SubjectGrade = await _courseSystemDB.SubjectGradeTypes.FindAsync(Id);
            if (SubjectGrade == null)
            {
                return new ServiceResponse(false, "SubjectGrade is null");
            }
            _courseSystemDB.SubjectGradeTypes.Remove(SubjectGrade);
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "delete success");
        }

        public async Task<PageResult<SubjectGradeTypeDTO>> GetSubjectGradeType(int page, int pagesize)
        {
            var query = _courseSystemDB.SubjectGradeTypes.AsQueryable();
            var count = await query.CountAsync();
            var SubjectGrades = await _courseSystemDB.SubjectGradeTypes.Include(sg => sg.Subject)
                                                      .Include(sg =>sg.GradeType)
                                                      .Include(sg => sg.CourseGroup)
                                                      .Skip((page-1)*pagesize)
                                                      .Take(pagesize)
                                                      .ToListAsync();
            var SubjectGradedto = _mapper.Map<List<SubjectGradeTypeDTO>>(SubjectGrades);
            return new PageResult<SubjectGradeTypeDTO>
            {
                Page = page,
                PageSize = pagesize,
                TotalRecoreds = count,
                TotalPages = (int)Math.Ceiling(count / (double)pagesize),
                Data = SubjectGradedto
            };
        }

        public async Task<SubjectGradeTypeDTO> GetSubjectGradeTypeById(int Id)
        {
            var SubjectGrade = await _courseSystemDB.SubjectGradeTypes.Include(sg => sg.Subject)
                                                      .Include(sg => sg.GradeType)
                                                      .Include(sg => sg.CourseGroup).FirstOrDefaultAsync(sg =>sg.Id == Id);
            if (SubjectGrade == null)
            {
                throw new ArgumentNullException( "SubjectGrade is null");
            }
            return _mapper.Map<SubjectGradeTypeDTO>(SubjectGrade);
        }

        public async Task<ServiceResponse> UpdateSubjectGradeType(int Id, SubjectGradeTypeDTO SubjectGradeTypeDTO)
        {
            var SubjectGrade = await _courseSystemDB.SubjectGradeTypes.FindAsync(Id);
            if (SubjectGrade == null)
            {
                return new ServiceResponse(false, "SubjectGrade is null");
            }
           SubjectGrade.SubjectId = SubjectGradeTypeDTO.SubjectId;
            SubjectGrade.MandatoryColumnGrade = SubjectGradeTypeDTO.MandatoryColumnGrade;
            SubjectGrade.GradeColumn = SubjectGradeTypeDTO.GradeColumn;
            SubjectGrade.CourseGroupId = SubjectGradeTypeDTO.CourseGroupId;
            SubjectGrade.GradeTypeId = SubjectGradeTypeDTO.GradeTypeId;
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "update success");
        }
    }
}
