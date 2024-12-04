using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class PermissionRepository : IPermissionService
    {
        private readonly CourseSystemDB _dbContext;
        private readonly IMapper _mapper;
        public PermissionRepository(CourseSystemDB dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<PermissionDTO> CreatePermission(PermissionDTO PermissionDTO)
        {
            var permission = _mapper.Map<Permission>(PermissionDTO);
            _dbContext.Permissions.Add(permission);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<PermissionDTO>(permission);
        }

        public async Task<ServiceResponse> DeletePermission(int Id)
        {
            var permission = await _dbContext.Permissions.FindAsync(Id);
            if (permission == null)
            {
                return new ServiceResponse (false,"permission is null");
            }
            _dbContext.Permissions.Remove(permission);
            await _dbContext.SaveChangesAsync();
            return new ServiceResponse(true, "delete success");
        }


        public async Task<List<PermissionDTO>> GetPermission()
        {
           var permissions = await _dbContext.Permissions.ToListAsync();
            return _mapper.Map<List<PermissionDTO>>(permissions);
        }

        public async Task<PermissionDTO> GetPermissionById(int Id)
        {
           var permissions = await _dbContext.Permissions.FindAsync(Id);
            if(permissions == null)
            {
                throw new Exception("Permission is null");
            }
            return _mapper.Map<PermissionDTO>(permissions);
        }


        public async Task<ServiceResponse> UpdatePermission(PermissionDTO PermissionDTO)
        {
            var permission = await _dbContext.Permissions.FindAsync(PermissionDTO.Id);
            if (permission == null)
            {
                return new ServiceResponse(false, "update don't success");
            }
            permission.PermissionName = PermissionDTO.Name;
            await _dbContext.SaveChangesAsync();
            return new ServiceResponse(true, "update success");
        }

        public async Task<ServiceResponse> UpdateRolePermission(RolePermissionDTO permissionDTO)
        {
           var permission = await _dbContext.RolePermissions.FindAsync(permissionDTO.Id);
            if(permission == null)
            {
                return new ServiceResponse(false,"role permission is null");
            }
            permission.PermissionId = permissionDTO.PermissionId;
            permission.RoleId = permissionDTO.RoleId;

            await _dbContext.SaveChangesAsync();
            return new ServiceResponse(true, "update success");
        }

        public async Task<ServiceResponse> DeleteRolePermission(int Id)
        {
            var permission = await _dbContext.RolePermissions.FindAsync(Id);
            if (permission == null)
            {
                return new ServiceResponse(false, "role permission is null");
            }
            _dbContext.RolePermissions.Remove(permission);
            await _dbContext.SaveChangesAsync();
            return new ServiceResponse(true, "delete success");
        }

        public async Task<List<RolePermissionDTO>> GetRolePermission()
        {
            var permission = await _dbContext.RolePermissions.ToListAsync();
            return _mapper.Map<List<RolePermissionDTO>>(permission);
        }

        public async Task<RolePermissionDTO> GetRolePermissionById(int Id)
        {
            var permission = await _dbContext.RolePermissions.FindAsync(Id);
            if (permission == null)
            {
                throw new Exception( "role permission is null");
            }
            return _mapper.Map<RolePermissionDTO>(permission);
        }

        public async Task<RolePermissionDTO> CreateRolePermission(RolePermissionDTO RolePermissionDTO)
        {
            var permission = _mapper.Map<RolePermission>(RolePermissionDTO);
            _dbContext.RolePermissions.Add(permission);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<RolePermissionDTO>(permission);
        }

    }
}
