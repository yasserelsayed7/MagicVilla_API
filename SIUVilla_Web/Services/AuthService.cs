using MagicVilla_Utility;
using SIUVilla_Web.Models.DTO;
using SIUVilla_Web.Services.IServices;

namespace SIUVilla_Web.Services
{
    public class AuthService : BaseService, IAuthService
    {

        private readonly IHttpClientFactory _ClientFactory;
        private string villaUrl;
        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            this._ClientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServicesUrls:VillaAPI");
        }
    
public Task<T> loginAsync<T>(LoginRequestDTO obj)
        {
            return SendAsync<T>(new Models.APIRequest
            {
                Data = obj,
                ApiType = SD.ApiType.POST,
                Url = villaUrl + "/api/UserAuth/Login"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDTO obj)
        {
            return SendAsync<T>(new Models.APIRequest
            {
                Data = obj,
                ApiType = SD.ApiType.POST,
                Url = villaUrl + "/api/UserAuth/Register"
            });
        }
    }
}
