using GisBackend.Core.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace GisBackend.Core.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<LayerEntity> Layers => Set<LayerEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LayerEntity>().Property(x => x.Name).HasMaxLength(100);
        }
    }
}
