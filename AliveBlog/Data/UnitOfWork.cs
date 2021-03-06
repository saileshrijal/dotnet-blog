using AliveBlog.Core.IConfiguration;
using AliveBlog.Core.IRepositories;
using AliveBlog.Core.Repositories;

namespace AliveBlog.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }
        public IPostRepository Post { get; private set; }
        public IPostCategoryRepository PostCategory { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Post = new PostRepository(_context);
            PostCategory = new PostCategoryRepository(_context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}