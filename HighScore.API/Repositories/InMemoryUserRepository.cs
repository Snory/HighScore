using HighScore.API.Models;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace HighScore.API.Repositories
{
    public class InMemoryUserRepository : IRepository<UserDTO>
    {
        private List<UserDTO> _data { get; set; }

        public InMemoryUserRepository() 
        {
            _data = new List<UserDTO>
            {
                new UserDTO { Id = 1, Name = "Bruce Willis"},
                new UserDTO { Id = 2, Name = "Jackie Chan"}
            };
        }

        public void Add(UserDTO item)
        {
            _data.Add(item);
        }

        public void Delete(UserDTO item)
        {
            _data.Remove(item);
        }

        public IEnumerable<UserDTO> Find(Expression<Func<UserDTO, bool>> predicate)
        {
            return _data
                   .AsQueryable()
                   .Where(predicate)
                   .ToList();
        }

        public IEnumerable<UserDTO> GetAll()
        {
            return _data;
        }
    }
}
