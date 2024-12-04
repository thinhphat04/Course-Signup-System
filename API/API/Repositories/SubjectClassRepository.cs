using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class SubjectClassRepository : ISubjectClassService
    {
        private readonly IMapper _mapper;
        private readonly CourseSystemDB _courseSystemDB;
        public SubjectClassRepository(IMapper mapper, CourseSystemDB courseSystemDB)
        {
            _mapper = mapper;
            _courseSystemDB = courseSystemDB;
        }

        public async Task<SubjectClassDTO> CreateSubjectClass(SubjectClassDTO SubjectClassDTO)
        {
           var subjectClass =  _mapper.Map<SubjectClass>(SubjectClassDTO);
            _courseSystemDB.SubjectClasses.Add(subjectClass);
            await _courseSystemDB.SaveChangesAsync();
            return _mapper.Map<SubjectClassDTO>(subjectClass);
        }

        public async Task<ServiceResponse> DeleteSubjectClass(int SubjectClassId)
        {
            var subjetClass = await _courseSystemDB.SubjectClasses.FindAsync(SubjectClassId);
            if(subjetClass == null)
            {
                return new ServiceResponse(false, "delete don't success");
            }
            _courseSystemDB.SubjectClasses.Remove(subjetClass);
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "delete success");
        }

        public async Task<PageResult<SubjectClassDTO>> GetSubjectClass(int page, int pagesize)
        {
            var query = _courseSystemDB.SubjectClasses.AsQueryable();
            var count = await query.CountAsync();
            var subjectClass = await query.Skip((page-1)*pagesize)
                                           .Take(pagesize).ToListAsync();
            var subjectClassDTO = _mapper.Map<List<SubjectClassDTO>>(subjectClass);
            return new PageResult<SubjectClassDTO>
            {
                Page = page,
                PageSize = pagesize,
                TotalPages = (int)Math.Ceiling(count / (double)pagesize),
                TotalRecoreds = count,
                Data = subjectClassDTO
            };
        }

        public async Task<SubjectClassDTO> GetSubjectClassById(int SubjectClassId)
        {
            var subjectClass = await _courseSystemDB.StudentClasses.FindAsync(SubjectClassId);
            if (subjectClass == null)
            {
                throw new ArgumentException("SubjectClass is null");
            }
            return _mapper.Map<SubjectClassDTO>(subjectClass);
        }

        public async Task<ServiceResponse> UpdateSubjectClass(int Id, SubjectClassDTO SubjectClassDTO)
        {
            var subjectClass = await _courseSystemDB.SubjectClasses.FindAsync(Id);
            if (subjectClass == null)
            {
                return new ServiceResponse(false, "Update don't success");
            }
            var sc = _mapper.Map<SubjectClass>(SubjectClassDTO);
            subjectClass.IsClose = sc.IsClose;
            subjectClass.ClassId =sc.ClassId;
            subjectClass.SubjectId =sc.SubjectId;
            subjectClass.DepartmentId =sc.DepartmentId;
            
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "Update success");
        }
    }
}
