using HighScore.Data.Context;
using HighScore.Domain.Entities;
using HighScore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HighScore.Data.Repositories
{
    public class EntityUserRepository : IRepository<UserDTO>
    {

        HighScoreContext _context;

        public EntityUserRepository(HighScoreContext HighScoreDBContext)
        {
            _context = HighScoreDBContext;
        }

        public async Task Add(UserDTO item)
        {
            //map dto to entity class
            User user = new User()
            {
                Name = item.Name
            };

            await _context.Users.AddAsync(user);
            _context.SaveChanges();
            
        }

        public Task Delete(UserDTO item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDTO>> Find(Expression<Func<UserDTO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDTO>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
