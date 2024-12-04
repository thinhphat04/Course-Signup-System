using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeSalaryRepository : IEmployeeSalaryService
    {
        private readonly IMapper _mapper;
        private readonly CourseSystemDB _db;
        public EmployeeSalaryRepository(IMapper mapper, CourseSystemDB db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task<EmployeeSalaryDTO> CreateSalary(EmployeeSalaryDTO employeeSalaryDTO)
        {
            var salary = _mapper.Map<EmployeeSalary>(employeeSalaryDTO);
            _db.EmployeeSalaries.Add(salary);
            await _db.SaveChangesAsync();
            return _mapper.Map<EmployeeSalaryDTO>(salary);
        }

        public async Task<ServiceResponse> DeleteSalary(int Id)
        {
            var salary = await _db.EmployeeSalaries.FindAsync(Id);
            if (salary == null)
            {
                return new ServiceResponse(false, "salary is null");
            }
            _db.EmployeeSalaries.Remove(salary);
            await _db.SaveChangesAsync();
            return new ServiceResponse(true, "delete success");
        }

        public async Task<PageResult<EmployeeSalaryDTO>> GetAllSalary(int page, int pagesize)
        {
            var query = _db.EmployeeSalaries.AsQueryable();
            var count = await query.CountAsync();
            var salary = await query.Skip((page-1)*pagesize).Take(pagesize).ToListAsync();
            var salaryDTO = _mapper.Map<List<EmployeeSalaryDTO>>(salary);
            return new PageResult<EmployeeSalaryDTO>
            {
                Page = page,
                PageSize = pagesize,
                TotalRecoreds = count,
                TotalPages = (int)Math.Ceiling(count / (double)pagesize),
                Data = salaryDTO
            };
        }

        public async Task<EmployeeSalaryDTO> GetSalaryById(int Id)
        {
            var salary = await _db.EmployeeSalaries.FindAsync(Id);
            if (salary == null)
            {
               throw new ArgumentNullException( "salary is null");
            }
            return _mapper.Map<EmployeeSalaryDTO>(salary);
        }

        public async Task<ServiceResponse> UpdateSalary(EmployeeSalaryDTO employeeSalaryDTO)
        {
            var salary = await _db.EmployeeSalaries.FindAsync(employeeSalaryDTO.EmployeeSalaryId);
            if (salary == null)
            {
                return new ServiceResponse(false, "salary is null");

            }
            else if(salary.IsClose == true)
            {
                return new ServiceResponse(false, "salary is close");
            }
            salary.Allowance = employeeSalaryDTO.Allowance;
            salary.AllowanceName = employeeSalaryDTO.AllowanceName;
            salary.Salary = employeeSalaryDTO.Salary;
            salary.SalaryReal = employeeSalaryDTO.SalaryReal;
            salary.UserId = employeeSalaryDTO.UserId;
            await _db.SaveChangesAsync();
            return new ServiceResponse(true, "update success");
        }
    }
}
