using FeatureBasic.src.Features.User;
using Microsoft.EntityFrameworkCore;

namespace FeatureBasic.src.Features.Data
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