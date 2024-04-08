using SIUVilla_Web.Models.DTO;

namespace SIUVilla_Web.Services.IServices
{
    public interface IAuthService
    {
        Task<T> loginAsync<T>(LoginRequestDTO obj);
        Task<T> RegisterAsync<T>(RegisterationRequestDTO obj);

    }
}
