using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class SubjectRepository : ISubjectService
    {
        private readonly IMapper _mapper;
        private readonly CourseSystemDB _courseSystemDB;
        public SubjectRepository(IMapper mapper, CourseSystemDB courseSystemDB)
        {
            _mapper = mapper;
            _courseSystemDB = courseSystemDB;
        }

        public async Task<SubjectDTO> CreateSubject(SubjectDTO SubjectDTO)
        {
            var subject = _mapper.Map<Subject>(SubjectDTO);
            
            _courseSystemDB.Subjects.Add(subject);
            await _courseSystemDB.SaveChangesAsync();
            return _mapper.Map<SubjectDTO>(subject);
        }

        public async Task<ServiceResponse> DeleteSubject(string SubjectId)
        {
            var subject = await _courseSystemDB.Subjects.FindAsync(SubjectId);
            if (subject == null)
            {
                return new ServiceResponse(false, "Subject is null");
            }
            _courseSystemDB.Subjects.Remove(subject);
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "Delete success");
        }

        public async Task<PageResult<SubjectDTO>> GetAllSubject(int page, int pagesize)
        {
            var query  = _courseSystemDB.Subjects.AsQueryable();
            var count = await query.CountAsync();
            var subjects = await query.Skip((page-1)*pagesize)
                                        .Take(pagesize).ToListAsync();
            var subjectDTO =  _mapper.Map<List<SubjectDTO>>(subjects);
            return new PageResult<SubjectDTO>
            {
                Page = page,
                PageSize = pagesize,
                TotalPages = (int)Math.Ceiling(count /(double)pagesize),
                TotalRecoreds = count,
                Data = subjectDTO
            };
        }

        public async Task<SubjectDTO> GetSubjectById(string SubjectId)
        {
            var subject = await _courseSystemDB.Subjects.FindAsync(SubjectId);
            if(subject == null)
            {
                throw new ArgumentException("subject is null");
            }
            return _mapper.Map<SubjectDTO>(subject);
        }

        public async Task<List<SubjectDTO>> SearchSubject(int DepartmentId, string FacultyId)
        {
            var subject = await _courseSystemDB.Subjects.Where(sb => sb.DepartmentId == DepartmentId
                                                                                && sb.FacultyId == FacultyId).ToListAsync();
             if (subject == null)
             {
                throw new ArgumentNullException("subject is null");
             }
             return _mapper.Map<List<SubjectDTO>>(subject);
        }

        public async Task<List<SubjectDTO>> SearchSubjectByName(string SubjectName)
        {
            var subject = await _courseSystemDB.Subjects.Where(sb => sb.SubjectName.Contains(SubjectName)).ToListAsync();
            if(subject == null)
            {
                throw new ArgumentException("subject is null");
            }
            return _mapper.Map<List<SubjectDTO>>(subject);
        }

        public async Task<ServiceResponse> UpdateSubject(SubjectDTO SubjectDTO)
        {
            var subject = await _courseSystemDB.Subjects.FindAsync(SubjectDTO.SubjectId);
            if (subject == null)
            {
                return new ServiceResponse(false, "subject is null");
            }
            var sj = _mapper.Map<Subject>(SubjectDTO);
            subject.FacultyId = sj.FacultyId;
            subject.SubjectName = sj.SubjectName;
            subject.FacultyId = sj.FacultyId;
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "update success");
        }
    }
}
