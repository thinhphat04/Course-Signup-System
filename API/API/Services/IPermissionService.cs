using API.DTO.Reponse;
using API.DTO;

namespace API.Services
{
    public interface IPermissionService
    {
        Task<PermissionDTO> CreatePermission(PermissionDTO PermissionDTO);
        Task<ServiceResponse> UpdatePermission(PermissionDTO PermissionDTO);
        Task<ServiceResponse> DeletePermission(int Id);
        Task<PermissionDTO> GetPermissionById(int Id);
        Task<List<PermissionDTO>> GetPermission();
        Task<RolePermissionDTO> CreateRolePermission(RolePermissionDTO RolePermissionDTO);
        Task<List<RolePermissionDTO>> GetRolePermission();
        Task<RolePermissionDTO> GetRolePermissionById(int Id);
        Task<ServiceResponse> DeleteRolePermission(int Id);
        Task<ServiceResponse> UpdateRolePermission(RolePermissionDTO RolePermissionDTO);


    }
}
