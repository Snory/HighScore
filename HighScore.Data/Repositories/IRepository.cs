using System.Linq.Expressions;

namespace HighScore.Data.Repositories
{
    public interface IRepository<T> where T: class
    {
        public Task Add(T item);

        public Task Delete(T item);

        public Task<IEnumerable<T>> GetAll();

        public Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    
    }
}
