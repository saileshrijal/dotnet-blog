using AliveBlog.Models;

namespace AliveBlog.Core.IRepositories
{
    public interface IPostCategoryRepository : IGenericRepository<PostCategory>
    {
        Task<List<PostCategory>> GetAllBlogForCategory(string category);
    }
}