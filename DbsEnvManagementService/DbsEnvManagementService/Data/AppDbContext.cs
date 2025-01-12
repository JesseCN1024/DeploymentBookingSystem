using Microsoft.EntityFrameworkCore;
using DbsEnvManagementService.Models.Domain;


namespace DbsEnvManagementService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<DbsEnvManagementService.Models.Domain.Env> Environments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("environment");

            //modelBuilder.

            //modelBuilder.Entity<Environment>().ToTable("Environments");
        }

    }
}

