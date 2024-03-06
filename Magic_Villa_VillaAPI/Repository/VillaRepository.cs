using Magic_Villa_VillaAPI.Models;
using Magic_Villa_VillaAPI.Data;
using Magic_Villa_VillaAPI.Repository.IRepositpory;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Magic_Villa_VillaAPI.Repository
{
    public class VillaRepository :Repository<Villa> ,IVillaRepository
    {
        private readonly ApplicatonDbContext _db;
        public VillaRepository(ApplicatonDbContext db) :base(db)
        {
            this._db = db;
        }
        //public async Task CreateAsync(Villa entity)
        //{
        //    await _db.AddAsync(entity);
        //    await SaveAsync();
        //}
        

        //public async Task<Villa> GetAsync(Expression<Func<Villa,bool>> filter = null, bool tracked = true)
        //{
        //    IQueryable<Villa> query = _db.Villas;
        //    if (!tracked)
        //    {
        //       query= query.AsNoTracking();
        //    }
        //    if (filter!=null)
        //    {
        //        query =query.Where(filter);
        //    }
        //    return await query.FirstOrDefaultAsync();

        //}

        //public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa,bool>> filter = null)
        //{
        //    IQueryable<Villa> query = _db.Villas;
        //    if (filter!=null)
        //    {
        //        query = query.Where(filter);
        //    }
        //    return await query.ToListAsync();
        //}

        //public async Task RemoveAsync(Villa entity)
        //{
        //       _db.Remove(entity);
        //        await SaveAsync();
        //}

        //public async Task SaveAsync()
        //{
        //    await _db.SaveChangesAsync();
        //}

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Update(entity);
            await SaveAsync();
            return entity;
        }
    }
}
