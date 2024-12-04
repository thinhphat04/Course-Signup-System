using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class DepartmentRepository : IDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly CourseSystemDB _db;
        public DepartmentRepository(IMapper mapper, CourseSystemDB db)
        { 
            _db = db;
            _mapper = mapper;
        }
        public async Task<DepartmentDTO> CreateDepartment(DepartmentDTO DepartmentDTO)
        {

            var department = _mapper.Map<Department>(DepartmentDTO);
             _db.Departments.Add(department);
            await _db.SaveChangesAsync();
            return _mapper.Map<DepartmentDTO>(department);
        }

        public async Task<ServiceResponse> DeleteDepartment(int Id)
        {
            var department = await _db.Departments.FindAsync(Id);
            if (department == null)
            {
                return new ServiceResponse(false, "department is null");
            }
            _db.Departments.Remove(department);
            await _db.SaveChangesAsync();
            return new ServiceResponse(true,"delete success");
        }

        public async Task<List<DepartmentDTO>> GetAllDepartment()
        {
            var departments = await _db.Departments.ToListAsync();
            return _mapper.Map<List<DepartmentDTO>>(departments);
        }

        public async Task<DepartmentDTO> GetDepartmentById(int Id)
        {
            var department = await _db.Departments.FindAsync(Id);
            if (department == null)
            {
                throw new Exception("in correct");
            }
            return _mapper.Map<DepartmentDTO>(department);
        }

        public async Task<ServiceResponse> UpdateDepartment(DepartmentDTO DepartmentDTO)
        {
            var department = await _db.Departments.FindAsync(DepartmentDTO.DepartmentId);
            if(department == null)
            {
                return new ServiceResponse(false,"department is null");
            }
            department.DepartmentName = DepartmentDTO.DepartmentName;
            await _db.SaveChangesAsync();
            return new ServiceResponse(true,"update success");
        }
    }
}
