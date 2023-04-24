using Identity.Provider.DTOs;
using IdentityProvider.DTOs;

namespace IdentityProvider.Services
{
    public interface IUserService
    {

        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegisterRequestDTO registerationRequestDTO);
        Task<bool> SaveChangesAsync();
        Task<bool> DeleteIdentityUser(string id);
    }
}
