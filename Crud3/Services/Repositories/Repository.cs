using BookReviewing_MVC.Data;
using BookReviewing_MVC.Services.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace BookReviewing_MVC.Services.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task Create(T entity)
        {
           await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter,bool tracked = true)
        {
            IQueryable<T> Query = _dbSet;
            
            Query = Query.Where(filter);
            
            if (!tracked)
            {
                Query = _dbSet.AsNoTracking();
            }
            return await Query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> Query = _dbSet;
            if (filter != null)
            {
                Query = Query.Where(filter);
            }      
            if (!tracked) 
            {
                Query = _dbSet.AsNoTracking();
            }
            return await Query.ToListAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
