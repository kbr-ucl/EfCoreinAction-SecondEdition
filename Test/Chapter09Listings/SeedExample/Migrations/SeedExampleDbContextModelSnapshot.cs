﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Test.Chapter09Listings.SeedExample;

namespace Test.Chapter09Listings.SeedExample.Migrations
{
    [DbContext(typeof(SeedExampleDbContext))]
    partial class SeedExampleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-preview.5.20278.2");

            modelBuilder.Entity("Test.Chapter09Listings.SeedExample.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProjectName")
                        .HasColumnType("TEXT");

                    b.HasKey("ProjectId");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            ProjectId = 1,
                            ProjectName = "Project1"
                        },
                        new
                        {
                            ProjectId = 2,
                            ProjectName = "Project2"
                        });
                });

            modelBuilder.Entity("Test.Chapter09Listings.SeedExample.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId");

                    b.HasIndex("ProjectId")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Name = "Jill",
                            ProjectId = 1
                        },
                        new
                        {
                            UserId = 2,
                            Name = "Jack",
                            ProjectId = 2
                        });
                });

            modelBuilder.Entity("Test.Chapter09Listings.SeedExample.User", b =>
                {
                    b.HasOne("Test.Chapter09Listings.SeedExample.Project", null)
                        .WithOne("ProjectManager")
                        .HasForeignKey("Test.Chapter09Listings.SeedExample.User", "ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Test.Chapter09Listings.SeedExample.SimpleAddress", "Address", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("City")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Street")
                                .HasColumnType("TEXT");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.HasData(
                                new
                                {
                                    UserId = 1,
                                    City = "city1",
                                    Street = "Jill street"
                                },
                                new
                                {
                                    UserId = 2,
                                    City = "city2",
                                    Street = "Jack street"
                                });
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
