using Magic_Villa_VillaAPI.Data;
using Magic_Villa_VillaAPI.Models;
using Magic_Villa_VillaAPI.Repository.IRepositpory;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Magic_Villa_VillaAPI.Repository
{
    public class Repository<T> :IRepository<T> where T : class
    {
        private readonly ApplicatonDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicatonDbContext db)
        {
            this._db = db;
            this.dbSet=_db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }
        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
            await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();

        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            _db.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
