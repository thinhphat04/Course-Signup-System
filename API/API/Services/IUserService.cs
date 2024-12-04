using API.DTO;
using API.DTO.Reponse;


namespace API.Services
{
    public interface IUserService
    {
        Task<UserDTO> CreateUser (UserDTO user);
        Task<ServiceResponse> UpdateUser (UserDTO user);
        Task<ServiceResponse> DeleteUser (string userId);
        Task<PageResult<UserDTO>> GetUser (int page, int pagesize);
        Task<UserDTO> GetUserById (string userId);
        Task<List<UserDTO>> GetUserByEmail (string Email);
           
    }
}
