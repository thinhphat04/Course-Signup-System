using API.DTO.Reponse;
using API.DTO;

namespace API.Services
{
    public interface IDepartmentService
    {
        Task<DepartmentDTO> CreateDepartment(DepartmentDTO DepartmentDTO);
        Task<List<DepartmentDTO>> GetAllDepartment();
        Task<ServiceResponse> DeleteDepartment(int Id);
        Task<DepartmentDTO> GetDepartmentById(int  Id);
        Task<ServiceResponse> UpdateDepartment(DepartmentDTO DepartmentDTO);
    }
}
