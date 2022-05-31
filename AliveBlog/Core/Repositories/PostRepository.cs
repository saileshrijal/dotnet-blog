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
            return await _context.Posts.Include(c => c.PostCategories).ThenInclude(c => c.Category).Include(c => c.Author).FirstOrDefaultAsync(predicate);
        }

        public async Task<List<Post>> GetAllBlogs()
        {
            return await _context.Posts.Where(p => p.IsPublished == true).Include(p => p.PostCategories).ThenInclude(p => p.Category).OrderByDescending(f => f.PublishedOn).Include(p => p.Author).ToListAsync();
        }

        public async Task<List<Post>> GetAllBlogsForBanner()
        {
            return await _context.Posts.Where(p => p.IsPublished == true && p.IsBanner == true).Include(p => p.PostCategories).ThenInclude(p => p.Category).OrderByDescending(f => f.PublishedOn).Include(p => p.Author).ToListAsync();
        }

        public async Task<List<Post>> GetAllBlogsForCategoryPage(Guid categoryId)
        {
            return await _context.Posts.Include(p => p.PostCategories).ThenInclude(p => p.Category).OrderByDescending(f => f.PublishedOn).Include(p => p.Author).Where(p => p.IsPublished == true).ToListAsync();
        }
    }
}