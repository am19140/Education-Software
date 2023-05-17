using Education_Software.Models;
using Microsoft.EntityFrameworkCore;

namespace Education_Software.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<UserModel> users { get; set; }
        public DbSet<SubjectModel> subjects { get; set; }
        public DbSet<SubjectModel> questions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserModel>().Property(x => x.username);
            modelBuilder.Entity<SubjectModel>().Property(x => x.sub_id).IsRequired();
            modelBuilder.Entity<QuestionModel>().Property(x => x.q_id).IsRequired();

        }


    }
}
