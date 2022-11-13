using HighScore.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace HighScore.Data.Context
{
    public class HighScoreContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<HighScoreEntity> HighScores { get; set; } = null!;

        //works but i have to run the migratons with more complicated cmdlet + add dependency to .API for EF
        //with factory EF can be run from package manager, but boy, i am not sure how to inject the context
        //public HighScoreContext(DbContextOptions<HighScoreContext> options) : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data source = sensei\\konoha; Initial Catalog=HighScore; Integrated Security=True;"); 
            options.LogTo(Console.WriteLine);
        }
    }
}
