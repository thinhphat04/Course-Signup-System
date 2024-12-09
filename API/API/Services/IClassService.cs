using API.DTO.Reponse;
using API.DTO;

namespace API.Services
{
    public interface IClassService
    {
        Task<ClassDTO> CreateClass(ClassDTO classDTO);
        Task<PageResult<ClassDTO>> GetAllClass(int page, int pagesize);
        Task<ServiceResponse> DeleteClass(string ClassId);
        Task<ClassDTO> GetClassById(string ClassId);

    }
}
