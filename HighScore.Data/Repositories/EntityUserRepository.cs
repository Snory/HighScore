using HighScore.Data.Context;
using HighScore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HighScore.Data.Repositories
{
    public class EntityUserRepository : IRepository<UserEntity>
    {
        private HighScoreContext _context;

        public EntityUserRepository(HighScoreContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> Add(UserEntity item)
        {
           await _context.Users.AddAsync(item);
           await SaveChanges();
           
           return item;
        }

        public async Task Delete(UserEntity item)
        {
            _context.Remove(item);
            await SaveChanges();
        }

        public async Task<IEnumerable<UserEntity>> Find(Expression<Func<UserEntity, bool>> predicate)
        {
            return await _context.Users.AsQueryable().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}
