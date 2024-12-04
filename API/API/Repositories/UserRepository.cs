using AutoMapper;
using API.DTO;
using API.DTO.Reponse;
using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace API.Repositories
{
    public class UserRepository : IUserService
    {
        private readonly CourseSystemDB _courseSystemDB;
        private readonly IMapper _mapper;
        private readonly IHashPasword _hashPasword;
        public UserRepository(CourseSystemDB courseSystemDB, IMapper mapper,IHashPasword hashPasword) 
        {
            _courseSystemDB = courseSystemDB;
            _mapper = mapper;
            _hashPasword = hashPasword;
        }
        public async Task<UserDTO> CreateUser(UserDTO userDto)
        {
            _hashPasword.CreateHashPassword(userDto.Password, out string HashPassword, out string PasswordSalt);
            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = HashPassword;
            user.PasswordSalt = PasswordSalt;          
            user.CreateAt = DateTime.Now;
            _courseSystemDB.Users.Add(user);
            await _courseSystemDB.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<ServiceResponse> DeleteUser(string userId)
        {
            var user = await _courseSystemDB.Users.FindAsync(userId);
            if(user is null)
            {
                return new ServiceResponse(false, "user Id is null");
            }
            _courseSystemDB.Remove(user);
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "Delete success");
        }

        public async Task<PageResult<UserDTO>> GetUser(int page, int pagesize)
        {
            var query = _courseSystemDB.Users.AsQueryable();
            var count = await query.CountAsync();
            var user = await query.Skip((page-1)*pagesize)
                                    .Take(pagesize).ToListAsync();
            var userdto =  _mapper.Map<List<UserDTO>>(user);
            return new PageResult<UserDTO>
            {
                Page = page,
                PageSize = pagesize,
                TotalRecoreds = count,
                TotalPages = (int)Math.Ceiling(count / (double)pagesize),
                Data = userdto
            };

        }

        public async Task<UserDTO> GetUserById(string Id)
        {
            var user = await _courseSystemDB.Users.FindAsync(Id);
            if(user is null)
            {
                throw new ArgumentNullException("user Id is null");
            }
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<ServiceResponse> UpdateUser(UserDTO userDTO)
        {
            var u = await _courseSystemDB.Users.FindAsync(userDTO.UserId);
            if(u is null)
            {
                return new ServiceResponse(false, "user Id is null");
            }
            var user = _mapper.Map<User>(userDTO);
          
            u.Email = user.Email;
            u.Avatar = user.Avatar;
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.RoleId = user.RoleId;
            u.UpdateAt = DateTime.Now;
            await _courseSystemDB.SaveChangesAsync();
            return new ServiceResponse(true, "Update success");
        }

        public async Task<List<UserDTO>> GetUserByEmail(string Email)
        {
                var user = await _courseSystemDB.Users.Where(r => r.Email == Email).ToListAsync();

                if (user is null)
                {
                    throw new ArgumentNullException("user is null");
                }
                return _mapper.Map<List<UserDTO>>(user);
                
        }
            

    }
}
