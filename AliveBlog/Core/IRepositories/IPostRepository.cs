using System.Linq.Expressions;
using AliveBlog.Models;

namespace AliveBlog.Core.IRepositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<Post?> GetWithCategoriesBy(Expression<Func<Post, bool>> predicate);
        Task<List<Post>> GetAllWithCategories(string UserId);
    }
}