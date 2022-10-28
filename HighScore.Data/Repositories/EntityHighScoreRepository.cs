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
    public class EntityHighScoreRepository : IRepository<HighScoreEntity>
    {
        HighScoreContext _context;
        public EntityHighScoreRepository(HighScoreContext context)
        {
            _context = context;
        }

        public async Task<HighScoreEntity> Add(HighScoreEntity item)
        {
            await _context.AddAsync(item);
            await SaveChanges();

            return item;
        }

        public async Task Delete(HighScoreEntity item)
        {
            _context.Remove(item);
            await SaveChanges();
        }

        public async Task<IEnumerable<HighScoreEntity>> Find(Expression<Func<HighScoreEntity, bool>> predicate)
        {
            return await _context.HighScores.AsQueryable().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<HighScoreEntity>> GetAll()
        {
            return await _context.HighScores.ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}
