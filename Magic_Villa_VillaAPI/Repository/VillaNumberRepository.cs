using Magic_Villa_VillaAPI.Data;
using Magic_Villa_VillaAPI.Models;
using Magic_Villa_VillaAPI.Repository.IRepositpory;

namespace Magic_Villa_VillaAPI.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicatonDbContext _db;
        public VillaNumberRepository(ApplicatonDbContext db) : base(db)
        {
            this._db = db;
        }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.VillaNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return  entity;

        }
    }
}
