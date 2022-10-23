using HighScore.API.Models;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace HighScore.API.Repositories
{
    public class InMemoryHighScoreRepository : IRepository<HighScoreDTO>
    {
        private List<HighScoreDTO> _data { get; set; }
        public InMemoryHighScoreRepository()
        {
            _data = new List<HighScoreDTO>()
            {
                new HighScoreDTO() { Id = 1, UserId = 1 , Score = 10 }
            };
        }

        public void Add(HighScoreDTO item)
        {
            _data.Add(item);
        }

        public void Delete(HighScoreDTO item)
        {
            _data.Remove(item);
        }

        public IEnumerable<HighScoreDTO> Find(Expression<Func<HighScoreDTO, bool>> predicate)
        {
            return _data
                   .AsQueryable()
                   .Where(predicate)
                   .ToList();
        }

        public IEnumerable<HighScoreDTO> GetAll()
        {
            return _data;
        }
    }
}
