using BookReviewing_MVC.Models;
using BookReviewing_MVC.Services.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T Get(Expression<Func<T, bool>> filter,bool tracked = true)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> filter, bool tracked = true)
        {
            IQueryable<T> Query;
            if (filter == null)
            {
                Query = _dbSet;
            }
            else 
            {
                Query = _dbSet.Where(filter);
            }
            return await Query.ToListAsync();
        }
    }
}
