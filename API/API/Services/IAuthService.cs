using API.DTO.Reponse;
using API.DTO.Request;

namespace API.Services
{
    public interface IAuthService
    {
        Task<string> Login(Login login);
   
    }
}
