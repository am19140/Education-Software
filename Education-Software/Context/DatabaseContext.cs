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
        public DbSet<QuestionModel> questions { get; set; }
        public DbSet<TestModel> tests { get; set; }
        public DbSet<ProgressModel> progress { get; set; }
        public DbSet<StatisticsModel> statistics { get; set; }
        public DbSet<GradesModel> grades { get; set; }
        public DbSet<QuestionnaireModel> questionnaire { get; set; }
        public DbSet<RecommendationModel> recommendations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserModel>().Property(x => x.username);
            modelBuilder.Entity<SubjectModel>().Property(x => x.sub_id).IsRequired();
            modelBuilder.Entity<QuestionModel>().Property(x => x.q_id).IsRequired();
            modelBuilder.Entity<TestModel>().HasKey(x => new { x.username,x.test_id, x.q_id});
            modelBuilder.Entity<TestModel>().Property(x => x.test_id).IsRequired();
            modelBuilder.Entity<TestModel>().Property(x => x.q_id).IsRequired();

            modelBuilder.Entity<ProgressModel>().HasKey(x => new { x.username, x.test_id });
            modelBuilder.Entity<ProgressModel>().Property(x => x.test_id).IsRequired();
            modelBuilder.Entity<StatisticsModel>().HasKey(x => new {x.username});

            modelBuilder.Entity<GradesModel>().HasKey(x => new { x.username, x.sub_id});
            modelBuilder.Entity<GradesModel>().Property(x => x.sub_id).IsRequired();
            modelBuilder.Entity<QuestionnaireModel>().Property(x => x.q_id).IsRequired();
            modelBuilder.Entity<RecommendationModel>().HasKey(x => new { x.username });
        }


    }
}
