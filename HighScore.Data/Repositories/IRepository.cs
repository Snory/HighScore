using HighScore.Data.Metadata;
using System.Linq.Expressions;

namespace HighScore.Data.Repositories
{
    public interface IRepository<T> where T: class
    {
        public Task<T> Add(T item);

        public Task Delete(T item);

        public Task<List<T>> Find(Expression<Func<T, bool>> filterPredicate);

        public Task<(List<T>, PaginationMetadata)> Find(Expression<Func<T, bool>> filterPredicate, Expression<Func<T, dynamic>> orderPredicate, string sorting, int pageNumber = 1, int pageSize = 20);

        public Task SaveChanges();


    }
}
