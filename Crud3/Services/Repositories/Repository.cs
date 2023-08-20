using BookReviewing_MVC.Data;
using BookReviewing_MVC.Services.IRepositories;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<T> Get(Expression<Func<T, bool>> filter,bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> Query = _dbSet;
            
            Query = Query.Where(filter);
            
            if (!tracked)
            {
                Query = _dbSet.AsNoTracking();
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    Query = Query.Include(includeProp);
                }
            }

            return await Query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, bool tracked = true,
             string[] includes = null)
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
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    Query = Query.Include(include);
                }
            }
            return await Query.ToListAsync();
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
