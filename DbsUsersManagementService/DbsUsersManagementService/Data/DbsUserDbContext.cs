using DbsUsersManagementService.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DbsUsersManagementService.Data
{
    public class DbsUserDbContext : DbContext
    {
        public DbsUserDbContext(DbContextOptions<DbsUserDbContext> dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Roles> Roles { get; set; }
        public DbSet<Teams> Teams { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed data for roles or something 
            // teams 

            // assuming all the things created by this usser: ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c

            var teams = new List<Teams>
            {
                new Teams
                {
                    Id = Guid.Parse("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
                    Name = "Mocha",
                    CreatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    UpdatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false
                },
                new Teams
                {
                    Id = Guid.Parse("f808ddcd-b5e5-4d80-b732-1ca523e48434"),
                    Name = "Latte",
                    CreatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    UpdatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false
                }
            };

            builder.Entity<Teams>().HasData(teams);

            // Roles

            var adminRoleId = "e4b257ef-3638-4156-ad81-c98692b06229";
            var powerUserRoleId = "1fbd0afe-4e8c-4b9a-8631-735107d30cb2";
            var generalUserRoleId = "37d00899-66a7-4ed2-b8da-6ee0f8395201";

            var roles = new List<Roles>
            {
                new Roles
                {
                    Id = Guid.Parse(adminRoleId),
                    Name = "ADMIN",
                    DisplayName = "Admin",
                    CreatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    UpdatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false
                },
                new Roles
                {
                    Id = Guid.Parse(powerUserRoleId),
                    Name = "POWER_USER",
                    DisplayName = "Power User",
                    CreatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    UpdatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false
                },
                new Roles
                {
                    Id = Guid.Parse(generalUserRoleId),
                    Name = "GENERAL_USER",
                    DisplayName = "General User",
                    CreatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    UpdatedBy = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false
                },

            };

            builder.Entity<Roles>().HasData(roles);


        }

    }
}
