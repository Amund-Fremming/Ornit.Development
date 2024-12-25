using FeatureResult.src.Features.User;
using Microsoft.EntityFrameworkCore;

namespace FeatureResult.src.Shared.AppData
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasKey(_ => _.ID);
        }
    }
}