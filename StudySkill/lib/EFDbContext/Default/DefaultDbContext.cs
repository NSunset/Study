using lib.Data.Default;
using lib.EntityConfiguration.Default;
using Microsoft.EntityFrameworkCore;

namespace lib.EFDbContext.Default
{
    public class DefaultDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public DefaultDbContext(DbContextOptions<DefaultDbContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
