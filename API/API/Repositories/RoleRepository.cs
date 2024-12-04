using AutoMapper;

using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RoleRepository : IRoleService
    {
        private readonly CourseSystemDB _courseDb;
        private readonly IMapper _mapper;
        public RoleRepository(CourseSystemDB courseDb,IMapper mapper) 
        {
            _courseDb = courseDb;
            _mapper = mapper;
        }
        public async Task<RoleDTO> CreateRole(RoleDTO roleDto)
        {
           if (roleDto == null)
           {
                throw new ArgumentNullException("Role is null");
           }
           var role = _mapper.Map<Role>(roleDto);
            _courseDb.Roles.Add(role);
            await _courseDb.SaveChangesAsync();
            return _mapper.Map<RoleDTO>(role);
        }

        public async Task<ServiceResponse> DeleteRole(int Id)
        {
            var roleId = await _courseDb.Roles.FindAsync(Id);
            if(roleId is null)
            {
                return new ServiceResponse(false,"Role Id is null");
            }
            _courseDb.Remove(roleId);
            await _courseDb.SaveChangesAsync();
            return new ServiceResponse(true, "Delete success");
        }

        public async Task<RoleDTO> GetRoleById(int Id)
        {
            var roleId = await _courseDb.Roles.FindAsync(Id);
            if (roleId is null)
            {
                throw new ArgumentNullException("Role Id is null");
            }
            return _mapper.Map<RoleDTO>(roleId);
        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            var Roles = await _courseDb.Roles.ToListAsync();
            return _mapper.Map<List<RoleDTO>>(Roles);
        }

        public async Task<ServiceResponse> UpdateRole(RoleDTO roleDTO)
        {
           var roleId = await _courseDb.Roles.FindAsync(roleDTO.RoleId);
           if( roleId is null)
           {
               return new ServiceResponse(false,"role Id is null");
           }
           roleId.RoleName = roleDTO.RoleName;
            await _courseDb.SaveChangesAsync();
            return new ServiceResponse(true, "update sucess");
        }
    }
}
