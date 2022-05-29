using System.Linq.Expressions;
using AliveBlog.Core.IRepositories;
using AliveBlog.Data;
using Microsoft.EntityFrameworkCore;

namespace AliveBlog.Core.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> entities;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        public async Task<bool> CheckExistBy(Expression<Func<T, bool>> predicate)
        {
            return await entities.AnyAsync(predicate);
        }

        public async Task Create(T t)
        {
            await entities.AddAsync(t);
        }

        public async Task Delete(Guid id)
        {
            var entity = await entities.FindAsync(id);
            if (entity != null)
            {
                entities.Remove(entity);
            }
        }

        public void Edit(T t)
        {
            entities.Update(t);
        }

        public async Task<List<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<List<T>> GetAllBy(Expression<Func<T, bool>> predicate)
        {
            return await entities.Where(predicate).ToListAsync();
        }

        public async Task<T?> GetBy(Expression<Func<T, bool>> predicate)
        {
            return await entities.FirstOrDefaultAsync(predicate);
        }
        public int TotalCount()
        {
            return entities.Count();
        }
    }
}