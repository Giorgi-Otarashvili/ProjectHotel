using Hotel.Repository.Data;
using Hotel.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hotel.Repository.Implementations
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity); 
            await SaveChangesAsync(); 
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity); 
            await SaveChangesAsync(); 
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity); 
            await SaveChangesAsync(); 
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync(); 
        }
    }
}
