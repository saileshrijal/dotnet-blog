using System.Linq.Expressions;
using AliveBlog.Core.IRepositories;
using AliveBlog.Core.Repository;
using AliveBlog.Data;
using AliveBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace AliveBlog.Core.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllWithCategories(string UserId)
        {
            return await _context.Posts.Where(c => c.AuthorId == UserId).Include(c => c.PostCategories).ThenInclude(c => c.Category).OrderByDescending(f => f.PublishedOn).ToListAsync();
        }
        public async Task<Post?> GetWithCategoriesBy(Expression<Func<Post, bool>> predicate)
        {
            return await _context.Posts.Include(c => c.PostCategories).ThenInclude(c => c.Category).FirstOrDefaultAsync(predicate);
        }
    }
}