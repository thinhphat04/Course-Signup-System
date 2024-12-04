using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class TeacherRepository : ITeacherService
    {
        private readonly CourseSystemDB _courseSystemDB;
        private readonly IMapper _mapper;
        private readonly IHashPasword _hashPasword;
        public TeacherRepository(CourseSystemDB courseSystemDB, IMapper mapper, IHashPasword hashPasword)
        {
            _courseSystemDB = courseSystemDB;
            _mapper = mapper;
            _hashPasword = hashPasword;
        }

        public async Task<TeacherDTO> CreateTeacher(TeacherDTO teacherdto)
        {
            _hashPasword.CreateHashPassword(teacherdto.Password, out string HashPassword, out string PasswordSalt);
            var teacher = _mapper.Map<Teacher>(teacherdto);
            teacher.PasswordHash = HashPassword;
            teacher.PasswordSalt = PasswordSalt;            
            teacher.CreateAt = DateTime.Now;
            teacher.UpdateAt = DateTime.Now;
            await  _courseSystemDB.Teachers.AddAsync(teacher);
            await _courseSystemDB.SaveChangesAsync();
            return _mapper.Map<TeacherDTO>(teacher);
        }

        public async Task<ServiceResponse> DeleteTeacher(string Id)
        {
            var teacher = await _courseSystemDB.Teachers.FindAsync(Id);
            if (teacher == null)
            {
                return new ServiceResponse(false, "teacher id is null");
            }
            _courseSystemDB.Users.Remove(teacher);
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "delete success");
        }

        public async Task<PageResult<TeacherDTO>> GetAllTeachers(int page, int pagesize)
                                                //page: Số trang bạn muốn lấy (bắt đầu từ 1).
                                                //pageSize: Số lượng bản ghi trên mỗi trang.
        {
            //Lấy toàn bộ bảng Teacher  dưới dạng IQueryable
            var query = _courseSystemDB.Teachers.AsQueryable();
            //Đếm tổng số bản ghi trong bảng Teacher bằng cách gọi CountAsync.
            var totalRecords = await query.CountAsync();

            var teachers = await query
                // Bỏ qua các bản ghi ở các trang trước
                .Skip((page - 1) * pagesize) // skip use with Iqueryable and AsQueryable
                //lấy đúng số lượng bản ghi theo kích thước của một trang.
                .Take(pagesize)
                .ToListAsync();        
            var teacherdto = _mapper.Map<List<TeacherDTO>>(teachers);
            return new PageResult<TeacherDTO>
            {
                TotalRecoreds = totalRecords,
                Page = page,
                PageSize = pagesize,
                TotalPages = (int)Math.Ceiling(totalRecords/ (double)pagesize), // Làm tròn lên đến số nguyên gần nhất
                Data = teacherdto
            };
        }

        public async Task<TeacherDTO> GetTeacherById(string id)
        {
            var teacher = await _courseSystemDB.Teachers.FindAsync(id);
            if (teacher is null)
            {
                throw new ArgumentException("Teacher id is null");
            }
            return _mapper.Map<TeacherDTO>(teacher);
        }

        public  async Task<List<TeacherDTO>> GetTeacherByEmail(string Email)
        {
            var teacher = await _courseSystemDB.Teachers.Where(r => r.Email == Email).ToListAsync();
            if (teacher is null)
            {
                throw new ArgumentNullException("stdent is null");
            }
            return _mapper.Map<List<TeacherDTO>>(teacher);
        }

        public async Task<ServiceResponse> UpdateTeacher(TeacherDTO teacherdto)
        {
            var teacherId = await _courseSystemDB.Teachers.FindAsync(teacherdto.UserId);
            if (teacherId is null)
            {
                return new ServiceResponse(false, "teacher Id is null");
            }
            var teacher = _mapper.Map<Teacher>(teacherdto);
            teacherId.Sex = teacher.Sex;
            teacherId.Email = teacher.Email;
            teacherId.FirstName = teacher.FirstName;
            teacherId.LastName = teacher.LastName;
            teacherId.PhoneNumber = teacher.PhoneNumber;
            teacherId.Avatar = teacher.Avatar;
            teacherId.Address = teacher.Address;
            teacherId.BirthDay = teacher.BirthDay;
            teacherId.UpdateAt = DateTime.Now;
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "Update success");
        }

        public async Task<List<TeacherDTO>> SearchTeacher(string Name)
        {
            var teachers = await _courseSystemDB.Teachers.Where(t =>             
               ( t.LastName + " " + t.FirstName).Contains(Name)).ToListAsync();
            // if( teachers)
            return _mapper.Map<List<TeacherDTO>>(teachers);
        }

        public async Task<TeacherSalary> GetSalaryOfTeacher(string TeacherId)
        {
            var classInfo = await _courseSystemDB.TeachSchedules.Include(ts => ts.Class)
                                        .Include(ts => ts.Class.StudentClasses)
                                       .Where(c => c.UserId == TeacherId)                                      
                                       .FirstOrDefaultAsync();   
            if (classInfo == null)
            {
                throw new Exception("teacher  don't teach class");
            }
            // Tổng học phí = học phí mỗi học sinh * số lượng học sinh
            var count  = classInfo!.Class.StudentClasses.Count();
            var totalReneve = classInfo.Class.Tuition * count;
            return new TeacherSalary
            {
                TotalRevenue = totalReneve,
                SalaryTeacher = totalReneve * count / 100
            };

        }
    }
}
