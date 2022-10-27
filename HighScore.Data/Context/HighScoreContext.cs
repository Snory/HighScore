using HighScore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighScore.Data.Context
{
    public class HighScoreContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<HighScore.Domain.Entities.HighScore> HighScores { get; set; } = null!;

        //public HighScoreContext(DbContextOptions<HighScoreContext> options) : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data source = sensei\\konoha; Initial Catalog=HighScore; Integrated Security=True;"); //why cant i use constructor?
        }
    }
}
