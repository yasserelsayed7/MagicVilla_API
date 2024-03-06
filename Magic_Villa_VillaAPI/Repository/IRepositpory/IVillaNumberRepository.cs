using Magic_Villa_VillaAPI.Models;

namespace Magic_Villa_VillaAPI.Repository.IRepositpory
{
    public interface IVillaNumberRepository :IRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateAsync(VillaNumber entity);
    }
}
