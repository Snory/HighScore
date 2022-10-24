using HighScore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace HighScore.Data.Repositories
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

        public async Task Add(HighScoreDTO item)
        {
            await Task.Delay(0); //ugly hack to have it async till i will find out how to do in memory with EF
            _data.Add(item);
        }

        public async Task Delete(HighScoreDTO item)
        {
            await Task.Delay(0); //ugly hack to have it async till i will find out how to do in memory with EF
            _data.Remove(item);
        }

        public async Task<IEnumerable<HighScoreDTO>> Find(Expression<Func<HighScoreDTO, bool>> predicate)
        {
            var data = await
                       _data
                       .AsQueryable()
                       .Where(predicate)
                       .ToListAsync();

            return data;
        }

        public async Task<IEnumerable<HighScoreDTO>> GetAll()
        {
            await Task.Delay(0); //ugly hack to have it async till i will find out how to do in memory with EF
            return _data;
        }
    }
}
