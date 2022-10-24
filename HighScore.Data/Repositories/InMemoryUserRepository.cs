using HighScore.Data.Repositories;
using HighScore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HighScore.Data.Repositories
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

        public async Task Add(UserDTO item)
        {
            await Task.Delay(0); //ugly hack to have it async till i will find out how to do in memory with EF
            _data.Add(item);
        }

        public async Task Delete(UserDTO item)
        {
            await Task.Delay(0); //ugly hack to have it async till i will find out how to do in memory with EF
            _data.Remove(item);
        }

        public async Task<IEnumerable<UserDTO>> Find(Expression<Func<UserDTO, bool>> predicate)
        {
            var data = await
                       _data
                       .AsQueryable()
                       .Where(predicate)
                       .ToListAsync();

            return data;

        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            await Task.Delay(0); //ugly hack to have it async till i will find out how to do in memory with EF
            return _data;
        }
         
    }
}
