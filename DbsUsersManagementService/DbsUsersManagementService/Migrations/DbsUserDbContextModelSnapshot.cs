﻿// <auto-generated />
using System;
using DbsUsersManagementService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DbsUsersManagementService.Migrations
{
    [DbContext(typeof(DbsUserDbContext))]
    partial class DbsUserDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DbsUsersManagementService.Models.Domain.Roles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e4b257ef-3638-4156-ad81-c98692b06229"),
                            CreatedBy = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            CreatedDate = new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5209),
                            DisplayName = "Admin",
                            IsDeleted = false,
                            Name = "ADMIN",
                            UpdatedBy = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            UpdatedDate = new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5209)
                        },
                        new
                        {
                            Id = new Guid("1fbd0afe-4e8c-4b9a-8631-735107d30cb2"),
                            CreatedBy = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            CreatedDate = new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5213),
                            DisplayName = "Power User",
                            IsDeleted = false,
                            Name = "POWER_USER",
                            UpdatedBy = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            UpdatedDate = new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5214)
                        },
                        new
                        {
                            Id = new Guid("37d00899-66a7-4ed2-b8da-6ee0f8395201"),
                            CreatedBy = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            CreatedDate = new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5216),
                            DisplayName = "General User",
                            IsDeleted = false,
                            Name = "GENERAL_USER",
                            UpdatedBy = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            UpdatedDate = new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5217)
                        });
                });

            modelBuilder.Entity("DbsUsersManagementService.Models.Domain.Teams", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            Id = new Guid("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
                            CreatedBy = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            CreatedDate = new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5123),
                            IsDeleted = false,
                            Name = "Mocha",
                            UpdatedBy = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            UpdatedDate = new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5133)
                        },
                        new
                        {
                            Id = new Guid("f808ddcd-b5e5-4d80-b732-1ca523e48434"),
                            CreatedBy = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            CreatedDate = new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5137),
                            IsDeleted = false,
                            Name = "Latte",
                            UpdatedBy = new Guid("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                            UpdatedDate = new DateTime(2024, 12, 5, 23, 16, 25, 84, DateTimeKind.Local).AddTicks(5138)
                        });
                });

            modelBuilder.Entity("DbsUsersManagementService.Models.Domain.UserRoles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UpdatedBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
