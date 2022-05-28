using System.Linq.Expressions;

namespace AliveBlog.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAllBy(Expression<Func<T, bool>> predicate);
        Task<T?> GetBy(Expression<Func<T, bool>> predicate);
        Task Create(T t);
        Task Delete(Guid id);
        void Edit(T t);
        Task<bool> CheckExistBy(Expression<Func<T, bool>> predicate);
        int TotalCount();
    }
}