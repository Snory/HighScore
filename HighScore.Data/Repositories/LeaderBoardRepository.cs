using HighScore.Data.Context;
using HighScore.Data.Metadata;
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
    public class LeaderBoardRepository : IRepository<LeaderBoardEntity>
    {
        private HighScoreContext _context;
        private const int _MAXPAGESIZE = 20;

        public LeaderBoardRepository(HighScoreContext context)
        {
            _context = context;
        }

        public async Task<LeaderBoardEntity> Add(LeaderBoardEntity item)
        {
            await _context.AddAsync(item);
            await SaveChanges();

            return item;
        }

        public async Task Delete(LeaderBoardEntity item)
        {
            _context.Remove(item);
            await SaveChanges();
        }

        public async Task<List<LeaderBoardEntity>> Find(Expression<Func<LeaderBoardEntity, bool>> filterPredicate)
        {
            var collectionToReturn = await
                    _context.LeaderBoards.AsQueryable()
                    .Where(filterPredicate)
                    .ToListAsync();


            return collectionToReturn;
        }

        public async Task<(List<LeaderBoardEntity>, PaginationMetadata)> Find(Expression<Func<LeaderBoardEntity, bool>> filterPredicate, Expression<Func<LeaderBoardEntity, dynamic>> orderPredicate, int pageNumber = 1, int pageSize = 20)
        {
            if (pageSize > _MAXPAGESIZE)
            {
                pageSize = _MAXPAGESIZE;
            }


            var totalItemCount = await _context.LeaderBoards.CountAsync();
            var paginationMetaData = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await
                    _context.LeaderBoards.AsQueryable()
                    .Where(filterPredicate)
                    .OrderByDescending(orderPredicate)
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .ToListAsync();

            return (collectionToReturn, paginationMetaData);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
