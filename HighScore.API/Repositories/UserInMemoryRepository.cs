using HighScore.API.Models;
using System.Linq.Expressions;

namespace HighScore.API.Repositories
{
    public class UserInMemoryRepository : IRepository<UserDTO>
    {
        private List<UserDTO> _users { get; set; }

        public static UserInMemoryRepository Instance { get; } = new UserInMemoryRepository();

        public UserInMemoryRepository()
        {
            _users = new List<UserDTO>
            {
                new UserDTO { Id = 1, Name = "Bruce Willis"},
                new UserDTO { Id = 2, Name = "Jackie Chan"}
            };
        }

        public IEnumerable<UserDTO> GetAll()
        {
            return _users;
        }

        public IEnumerable<UserDTO> Find(Expression<Func<UserDTO, bool>> predicate)
        {
            return _users
                   .AsQueryable()
                   .Where(predicate)
                   .ToList();
        }

        public void Add(UserDTO item)
        {
            _users.Add(item);
        }

        public void Delete(UserDTO item)
        {
            _users.Remove(item);
        }
    }
}
