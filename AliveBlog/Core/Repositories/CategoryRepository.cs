using AliveBlog.Core.IRepositories;
using AliveBlog.Core.Repository;
using AliveBlog.Data;
using AliveBlog.Models;

namespace AliveBlog.Core.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}