using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class FacultyRepository : IFacultyService
    {
        private readonly IMapper _mapper;
        private readonly CourseSystemDB _courseSystemDB;
        public FacultyRepository(IMapper mapper, CourseSystemDB courseSystemDB)
        {
            _mapper = mapper;
            _courseSystemDB = courseSystemDB;
        }

        public async Task<FacultyDTO> CreateFaculty(FacultyDTO facultyDTO)
        {
           var faculty = _mapper.Map<Faculty>(facultyDTO);
            faculty.CreateAt = DateTime.Now;
            _courseSystemDB.Faculties.Add(faculty);
            await _courseSystemDB.SaveChangesAsync();
            return _mapper.Map<FacultyDTO>(faculty);
        }

        public async Task<ServiceResponse> DeleteFaculty(string facultyId)
        {
            var faculty = await _courseSystemDB.Faculties.FindAsync(facultyId);
            if (faculty == null)
            {
                return new ServiceResponse(false, "delete don't success");
            }
            _courseSystemDB.Faculties.Remove(faculty);
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "delete success");
        }

        public async Task<PageResult<FacultyDTO>> GetAllFaculty(int page, int pagesize)
        {
            var query = _courseSystemDB.Faculties.AsQueryable();
            var count = await query.CountAsync();
            var faculty = await query.Skip((page-1)*pagesize)
                                    .Take(pagesize).ToListAsync();
            var facultyDTO =  _mapper.Map<List<FacultyDTO>>(faculty);
            return new PageResult<FacultyDTO>
            {
                Page = page,
                PageSize = pagesize,
                TotalRecoreds = count,
                TotalPages = (int)Math.Ceiling(count / (double)pagesize),
                Data = facultyDTO
            };
        }

        public async Task<FacultyDTO> GetFacultyById(string facultyId)
        {
            var faculty = await _courseSystemDB.Faculties.FindAsync(facultyId);
            if (faculty == null)
            {
                throw new ArgumentNullException("faculty is null");
            }
            return _mapper.Map<FacultyDTO>(faculty);
        }

        public async Task<ServiceResponse> UpdateFaculty(FacultyDTO facultyDTO)
        {
            var faculty = await _courseSystemDB.Faculties.FindAsync(facultyDTO.FacultyId);
            if (faculty == null)
            {
                return new ServiceResponse(false,"faculty is null");
            }
            var facultyId = _mapper.Map<Faculty>(facultyDTO);
            faculty.UpdateAt = DateTime.Now;
            faculty.FacultyName = facultyId.FacultyName;
            return new ServiceResponse(true, "update success");

        }
    }
}
