using SIUVilla_Web.Models;

namespace SIUVilla_Web.Services.IServices
{
    public interface IBaseService
    {
        public APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
