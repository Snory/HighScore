using System.Linq.Expressions;

namespace HighScore.API.Repositories
{
    public interface IRepository<T> where T: class
    {
        public IEnumerable<T> GetAll();

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    
    }
}
