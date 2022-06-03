using AliveBlog.Core.IRepositories;
using AliveBlog.Core.Repository;
using AliveBlog.Data;
using AliveBlog.Models;
using Microsoft.EntityFrameworkCore;

namespace AliveBlog.Core.Repositories
{
    public class PostCategoryRepository : GenericRepository<PostCategory>, IPostCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public PostCategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<PostCategory>> GetAllBlogForCategory(string category)
        {
            return await _context.PostCategories.Include(pc => pc.Post).ThenInclude(pc => pc.Author).Include(pc => pc.Post).ThenInclude(pc => pc.PostCategories).ThenInclude(pc => pc.Category).Where(pc => pc.Category.Title == category).ToListAsync();
        }
    }
}