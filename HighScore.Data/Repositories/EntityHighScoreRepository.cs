using HighScore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HighScore.Data.Repositories
{
    public class EntityHighScoreRepository : IRepository<HighScoreDTO>
    {
        public Task Add(HighScoreDTO item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(HighScoreDTO item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HighScoreDTO>> Find(Expression<Func<HighScoreDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HighScoreDTO>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
