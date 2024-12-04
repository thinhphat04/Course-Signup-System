using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace API.Repositories
{
    public class StudentClassRepository : IStudentClassService
    {
        private readonly IMapper _mapper;
        private readonly CourseSystemDB _courseSystemDB;
        public StudentClassRepository(IMapper mapper, CourseSystemDB courseSystemDB)
        {
            _mapper = mapper;
            _courseSystemDB = courseSystemDB;
        }
        public async Task<StudentClassDTO> CreateStudentClass(StudentClassDTO studentClassDTO)
        {
            var studentclass = _mapper.Map<StudentClass>(studentClassDTO);
            studentclass.CreateAt = DateTime.Now;
             _courseSystemDB.StudentClasses.Add(studentclass);
            await _courseSystemDB.SaveChangesAsync();
            return _mapper.Map<StudentClassDTO>(studentclass);
        }

        public async Task<ServiceResponse> DeleteStudentClass(int StudentClassId)
        {
            var studentclass = await _courseSystemDB.StudentClasses.FindAsync(StudentClassId);
            if (studentclass == null)
            {
                return new ServiceResponse(false, "id incorrect");
            }
            _courseSystemDB.StudentClasses.Remove(studentclass);
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "delete success");
        }

        public async Task<StudentClassDTO> GetStudentClassById(int StudentClassId)
        {
            var studentclass = await _courseSystemDB.StudentClasses.Where(sc => sc.StudentClassId == StudentClassId)
                                                .Select(sc => new StudentClassDTO
                                                {
                                                    Status = sc.Status,
                                                    UserId = sc.UserId,
                                                    ClassId = sc.ClassId,
                                                    Name = sc.Student.LastName + " " + sc.Student.FirstName, // Lấy tên đầy đủ
                                                    ClassName = sc.Class.ClassName                          // Lấy tên lớp
                                                })
                                                .FirstOrDefaultAsync();
            if (studentclass == null)
            {
                throw new ArgumentNullException( "student class is null");
            }
            return _mapper.Map<StudentClassDTO>(studentclass);
        }

        public async Task<PageResult<StudentClassDTO>> GetStudentClasses(int page, int pagesize)
        {
          
            var query = _courseSystemDB.StudentClasses.AsQueryable();
            var count = await query.CountAsync();
            var studentclass = await query.Skip((page-1)*pagesize)
                                        .Take(pagesize)
                                   .Select(st => new StudentClassDTO
                                   {
                                       Name = st.Student.LastName + " " + st.Student.FirstName,
                                       ClassName = st.Class.ClassName,
                                       UserId = st.UserId,
                                       ClassId = st.ClassId,
                                       Status = st.Status
                                   })
                                   .ToListAsync();
            var studentclassDTO = _mapper.Map<List<StudentClassDTO>>(studentclass);
            return new PageResult<StudentClassDTO>
            {
                Page = page,
                PageSize = pagesize,
                TotalRecoreds = count,
                TotalPages = (int)Math.Ceiling(count / (double)pagesize),
                Data = studentclassDTO
            };
        }

        public async Task<ServiceResponse> UpdateStudentClass(int Id,StudentClassDTO studentClassDTO)
        {
            var studentclass = await _courseSystemDB.StudentClasses.FindAsync(Id);
            if (studentclass == null)
            {
                throw new ArgumentNullException("student class is null");
            }
            var sc = _mapper.Map<StudentClass>(studentClassDTO);
            studentclass.Status = sc.Status;
            studentclass.ClassId = sc.ClassId;
            studentclass.UserId = sc.UserId;
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "update success");
        }

        public async Task<List<StudentClassDTO>> GetStudentByStatus(bool status)
        {
            var studentClass = await _courseSystemDB.StudentClasses.Where(st => st.Status == status)
                                                     .Select(st => new StudentClassDTO
                                                    {
                                                        Name = st.Student.LastName + " " + st.Student.FirstName,
                                                        ClassName = st.Class.ClassName,
                                                        UserId = st.UserId,
                                                        ClassId = st.ClassId,
                                                        Status = st.Status
                                                    }).ToListAsync();
            if(studentClass == null)
            {
                throw new ArgumentNullException("Student is null");
            }
            return _mapper.Map<List<StudentClassDTO>>(studentClass);
        }

        public async Task<ServiceResponse> CheckTuitionFeePayment(string StudentId, string ClassId)
        {
          var studentClass = await _courseSystemDB.StudentClasses    
                                            .FirstOrDefaultAsync(sc =>sc.UserId ==StudentId && sc.ClassId == ClassId);
            if (studentClass == null)
            {
                return new ServiceResponse(false, "Student is null");
            }
            if (studentClass.Status == false)
            {
                return new ServiceResponse(false, "Student has not paid tuition for this class.");
            }
            else
            {
                return new ServiceResponse(true, "Student has registered and paid tuition.");
            }

        }
    }
}
