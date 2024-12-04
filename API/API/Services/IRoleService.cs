using API.DTO;
using API.DTO.Reponse;
using API.Entities;

namespace API.Services
{
    public interface IRoleService
    {
        Task<RoleDTO> CreateRole(RoleDTO role);
        Task<ServiceResponse> UpdateRole(RoleDTO role);
        Task<ServiceResponse> DeleteRole(int Id);
        Task<RoleDTO> GetRoleById(int Id);
        Task<List<RoleDTO>> GetRoles();
    }
}
