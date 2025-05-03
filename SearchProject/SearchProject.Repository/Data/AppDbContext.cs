using Microsoft.EntityFrameworkCore;
using SearchProject.Entities;

namespace SearchProject.Repository.Data
{
    /// <summary>
    /// App Db context class
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// overriding the on model creation
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();
            modelBuilder.Entity<User>()
                .HasMany(u => u.SearchHistories)
                .WithOne(sh => sh.User)
                .HasForeignKey(sh => sh.UserId);
        }

        /// <summary>
        /// Movies db set
        /// </summary>
        public DbSet<Movies> Movies { get; set; }

        /// <summary>
        /// search histories db set
        /// </summary>
        public DbSet<SearchHistory> SearchHistories { get; set; }

        /// <summary>
        /// user db set
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// Eroor reports db set
        /// </summary>
        public DbSet<ErrorReport> ErrorReports { get; set; }
    }
}
