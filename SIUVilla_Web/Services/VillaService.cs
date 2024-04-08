using MagicVilla_Utility;
using SIUVilla_Web.Models;
using SIUVilla_Web.Models.DTO;
using SIUVilla_Web.Services.IServices;

namespace SIUVilla_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {

        private readonly IHttpClientFactory _ClientFactory;
        private string villaUrl;
        public VillaService(IHttpClientFactory clientFactory , IConfiguration configuration) : base(clientFactory) 
        {
            this._ClientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServicesUrls:VillaAPI");
        }
        public Task<T> CreateAsync<T>(VillaCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl +"/api/villaAPI",
                Token = token
            }
            ); 
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.DELETE,
                
                Url = villaUrl + "/api/villaAPI/"+id,
                Token = token
            }
             );
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.GET,

                Url = villaUrl + "/api/villaAPI",
                Token = token
            }
            );
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.GET,

                Url = villaUrl + "/api/villaAPI/" + id,
                Token = token
            }
            );
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + "/api/villaAPI/"+dto.Id
                ,Token = token
            }
           );
        }
    }
}
