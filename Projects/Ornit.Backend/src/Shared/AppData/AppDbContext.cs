using Microsoft.EntityFrameworkCore;
using Ornit.Backend.src.Features.User;

namespace Ornit.Backend.src.Shared.AppData
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Examples { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>()
                .HasKey(_ => _.Id);
        }
    }
}