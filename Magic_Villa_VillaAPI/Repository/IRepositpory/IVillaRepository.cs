using Magic_Villa_VillaAPI.Models;
using System.Linq.Expressions;

namespace Magic_Villa_VillaAPI.Repository.IRepositpory
{
    public interface IVillaRepository :IRepository<Villa>
    {
       
        Task<Villa> UpdateAsync(Villa entity);
    }
}
