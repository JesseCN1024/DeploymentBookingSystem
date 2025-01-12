using DbsUsersManagementService.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DbsUsersManagementService.Data
{
    public class DbsUserAuthDbContext : DbContext
    {
        public DbsUserAuthDbContext(DbContextOptions<DbsUserAuthDbContext> options) : base(options)
        {


        }

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           
            var teams = new List<Team>
            {
                new Team
                {
                    Id = Guid.Parse("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
                    Name = "Mocha",
                    CreatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    UpdatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Team
                {
                    Id = Guid.Parse("f808ddcd-b5e5-4d80-b732-1ca523e48434"),
                    Name = "Latte",
                    CreatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    UpdatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false
                }
            };

            modelBuilder.Entity<Team>().HasData(teams);

            // Roles

            var adminRoleId = "e4b257ef-3638-4156-ad81-c98692b06229";
            var powerUserRoleId = "1fbd0afe-4e8c-4b9a-8631-735107d30cb2";
            var generalUserRoleId = "37d00899-66a7-4ed2-b8da-6ee0f8395201";

            var roles = new List<Role>
            {
                new Role
                {
                    Id = Guid.Parse(adminRoleId),
                    Name = "ADMIN",
                    DisplayName = "Admin",
                    CreatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    UpdatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Role
                {
                    Id = Guid.Parse(powerUserRoleId),
                    Name = "POWER_USER",
                    DisplayName = "Power User",
                    CreatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    UpdatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },
                new Role
                {
                    Id = Guid.Parse(generalUserRoleId),
                    Name = "GENERAL_USER",
                    DisplayName = "General User",
                    CreatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    UpdatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsDeleted = false
                },

            };

            modelBuilder.Entity<Role>().HasData(roles);

            modelBuilder.HasDefaultSchema("identity");


        }

    }
}
