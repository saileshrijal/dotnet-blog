using AliveBlog.Core.IRepositories;

namespace AliveBlog.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IPostRepository Post { get; }
        IPostCategoryRepository PostCategory { get; }
        Task SaveAsync();
    }
}