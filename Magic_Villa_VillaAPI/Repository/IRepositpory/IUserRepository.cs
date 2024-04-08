using Magic_Villa_VillaAPI.Models;
using Magic_Villa_VillaAPI.Models.DTO;

namespace Magic_Villa_VillaAPI.Repository.IRepositpory
{
    public interface IUserRepository
    {
        bool IsUniqueUser (string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
         Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO );
    }
}
