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
    public class EntityUserRepository : IRepository<UserEntity>
    {
        private HighScoreContext _context;
        private const int _MAXPAGESIZE = 20;

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

        public async Task<(List<UserEntity>, PaginationMetadata)> Find(Expression<Func<UserEntity, bool>> filterPredicate, Expression<Func<UserEntity, dynamic>> orderPredicate, int pageNumber = 1 , int pageSize = 20)
        {
            if (pageSize > _MAXPAGESIZE)
            {
                pageSize = _MAXPAGESIZE;
            }


            var totalItemCount = await _context.Users.CountAsync();
            var paginationMetaData = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await
                    _context.Users.AsQueryable()
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

        public async Task<List<UserEntity>> Find(Expression<Func<UserEntity, bool>> filterPredicate)
        {
            return await
                    _context.Users.AsQueryable()
                    .Where(filterPredicate)
                    .ToListAsync();
        }
    }
}
