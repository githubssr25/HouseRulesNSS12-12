﻿// <auto-generated />
using System;
using HouseRules.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HouseRules.Migrations
{
    [DbContext(typeof(HouseRulesDbContext))]
    [Migration("20241216192123_AddNewUserSsr25")]
    partial class AddNewUserSsr25
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HouseRules.Models.Chore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChoreFrequencyDays")
                        .HasColumnType("integer");

                    b.Property<int>("Difficulty")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Chores");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChoreFrequencyDays = 14,
                            Difficulty = 4,
                            Name = "Mow the Lawn"
                        },
                        new
                        {
                            Id = 2,
                            ChoreFrequencyDays = 7,
                            Difficulty = 2,
                            Name = "Clean the Kitchen"
                        },
                        new
                        {
                            Id = 3,
                            ChoreFrequencyDays = 1,
                            Difficulty = 1,
                            Name = "Take Out Trash"
                        },
                        new
                        {
                            Id = 4,
                            ChoreFrequencyDays = 7,
                            Difficulty = 3,
                            Name = "Vacuum the House"
                        },
                        new
                        {
                            Id = 5,
                            ChoreFrequencyDays = 30,
                            Difficulty = 5,
                            Name = "Wash the Car"
                        },
                        new
                        {
                            Id = 6,
                            ChoreFrequencyDays = 2,
                            Difficulty = 2,
                            Name = "Water the Plants"
                        },
                        new
                        {
                            Id = 7,
                            ChoreFrequencyDays = 10,
                            Difficulty = 4,
                            Name = "Clean the Bathroom"
                        },
                        new
                        {
                            Id = 8,
                            ChoreFrequencyDays = 14,
                            Difficulty = 2,
                            Name = "Dust the Shelves"
                        },
                        new
                        {
                            Id = 9,
                            ChoreFrequencyDays = 60,
                            Difficulty = 5,
                            Name = "Organize the Garage"
                        },
                        new
                        {
                            Id = 10,
                            ChoreFrequencyDays = 1,
                            Difficulty = 3,
                            Name = "Cook Dinner"
                        });
                });

            modelBuilder.Entity("HouseRules.Models.ChoreAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChoreId")
                        .HasColumnType("integer");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChoreId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("ChoreAssignments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChoreId = 1,
                            UserProfileId = 1
                        },
                        new
                        {
                            Id = 2,
                            ChoreId = 2,
                            UserProfileId = 1
                        },
                        new
                        {
                            Id = 3,
                            ChoreId = 3,
                            UserProfileId = 1
                        },
                        new
                        {
                            Id = 4,
                            ChoreId = 4,
                            UserProfileId = 1
                        });
                });

            modelBuilder.Entity("HouseRules.Models.ChoreCompletion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChoreId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CompletedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChoreId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("ChoreCompletions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChoreId = 1,
                            CompletedOn = new DateTime(2024, 12, 2, 13, 21, 23, 422, DateTimeKind.Local).AddTicks(1560),
                            UserProfileId = 1
                        },
                        new
                        {
                            Id = 2,
                            ChoreId = 2,
                            CompletedOn = new DateTime(2024, 12, 9, 13, 21, 23, 422, DateTimeKind.Local).AddTicks(1600),
                            UserProfileId = 1
                        },
                        new
                        {
                            Id = 3,
                            ChoreId = 3,
                            CompletedOn = new DateTime(2024, 12, 15, 13, 21, 23, 422, DateTimeKind.Local).AddTicks(1600),
                            UserProfileId = 1
                        },
                        new
                        {
                            Id = 4,
                            ChoreId = 4,
                            CompletedOn = new DateTime(2024, 12, 9, 13, 21, 23, 422, DateTimeKind.Local).AddTicks(1630),
                            UserProfileId = 1
                        });
                });

            modelBuilder.Entity("HouseRules.Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("IdentityUserId")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdentityUserId");

                    b.ToTable("UserProfiles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "101 Main Street",
                            Email = "admin.strator123@gmail.com",
                            FirstName = "Admina",
                            IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                            LastName = "Strator"
                        },
                        new
                        {
                            Id = 2,
                            Address = "300 Admin Street",
                            Email = "sr25practice@example.com",
                            FirstName = "Secondssr25",
                            IdentityUserId = "456e7b9a-a12f-4bcb-a9e7-a2c5e764d234",
                            LastName = "Adminssr25"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                            Name = "Admin",
                            NormalizedName = "admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "5a5a70c3-f9d4-4631-9dd7-a3a8d7e065cb",
                            Email = "admina@strator.comx",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAIAAYagAAAAEHi3YTB8FYeDTKsbH/lV7u5tXc3mk23UYA+Brb+7Nl42hUo7wVQtzS5+sOEqSccMWA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "0dafc71b-349c-4424-a297-5d61d5c0e46b",
                            TwoFactorEnabled = false,
                            UserName = "Administrator"
                        },
                        new
                        {
                            Id = "456e7b9a-a12f-4bcb-a9e7-a2c5e764d234",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "2f5d0cf2-f6e9-40b8-9db1-34b53c1a9532",
                            Email = "sr25practice@example.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAIAAYagAAAAEHRayzGu0seokYHyYYGvokw5PzSwwt+L/0feeRdt4iHXEL7dozx0p8Co8dDNiu5C8g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "b3f069c1-9c58-4f4c-886f-51b83f74494e",
                            TwoFactorEnabled = false,
                            UserName = "ssr25practice"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                            RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35"
                        },
                        new
                        {
                            UserId = "456e7b9a-a12f-4bcb-a9e7-a2c5e764d234",
                            RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HouseRules.Models.ChoreAssignment", b =>
                {
                    b.HasOne("HouseRules.Models.Chore", "Chore")
                        .WithMany("ChoreAssignments")
                        .HasForeignKey("ChoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HouseRules.Models.UserProfile", "UserProfile")
                        .WithMany("ChoreAssignments")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chore");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("HouseRules.Models.ChoreCompletion", b =>
                {
                    b.HasOne("HouseRules.Models.Chore", "Chore")
                        .WithMany("ChoreCompletions")
                        .HasForeignKey("ChoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HouseRules.Models.UserProfile", "UserProfile")
                        .WithMany("ChoreCompletions")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chore");

                    b.Navigation("UserProfile");
                });

            modelBuilder.Entity("HouseRules.Models.UserProfile", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "IdentityUser")
                        .WithMany()
                        .HasForeignKey("IdentityUserId");

                    b.Navigation("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HouseRules.Models.Chore", b =>
                {
                    b.Navigation("ChoreAssignments");

                    b.Navigation("ChoreCompletions");
                });

            modelBuilder.Entity("HouseRules.Models.UserProfile", b =>
                {
                    b.Navigation("ChoreAssignments");

                    b.Navigation("ChoreCompletions");
                });
#pragma warning restore 612, 618
        }
    }
}
