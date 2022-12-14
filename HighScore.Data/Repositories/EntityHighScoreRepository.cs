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
    public class EntityHighScoreRepository : IRepository<HighScoreEntity>
    {
        private HighScoreContext _context;
        private const int _MAXPAGESIZE = 20;
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

        public async Task<(List<HighScoreEntity>, PaginationMetadata)> Find(Expression<Func<HighScoreEntity, bool>> filterPredicate, Expression<Func<HighScoreEntity, dynamic>> orderPredicate, string sorting, int pageNumber, int pageSize)
        {
            if (pageSize > _MAXPAGESIZE)
            {
                pageSize = _MAXPAGESIZE;
            }

            var totalItemCount = await _context.HighScores.CountAsync();
            var paginationMetaData = new PaginationMetadata(totalItemCount, pageSize, pageNumber);


            var query =
                     _context.HighScores.AsQueryable()
                     .Where(filterPredicate);
   

            if(sorting == "asc")
            {
                query = query.OrderBy(orderPredicate);
            } else
            {
                query = query.OrderByDescending(orderPredicate); //if they are not able to get asc/desc right, sort it by desc :D
            }

            var collectionToReturn = await
                    query.Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .ToListAsync();

            return (collectionToReturn, paginationMetaData);

        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<HighScoreEntity>> Find(Expression<Func<HighScoreEntity, bool>> filterPredicate)
        {
            return await
                    _context.HighScores.AsQueryable()
                    .Where(filterPredicate)
                    .ToListAsync();
        }
    }
}
