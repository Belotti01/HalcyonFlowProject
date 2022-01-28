﻿// <auto-generated />
using System;
using HalcyonFlowProject.Data.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HalcyonFlowProject.Migrations
{
    [DbContext(typeof(DB))]
    partial class DBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.ProjectTask", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint")
                        .HasColumnName("fk_creator");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<long>("TicketId")
                        .HasColumnType("bigint")
                        .HasColumnName("fk_ticket");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<bool>("CanAssignTasks")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("can_assign_tasks");

                    b.Property<bool>("CanComment")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("can_comment");

                    b.Property<bool>("CanCreateProjects")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("can_create_projects");

                    b.Property<bool>("CanCreateTasks")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("can_create_tasks");

                    b.Property<bool>("CanCreateTickets")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("can_create_tickets");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAdministrator")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_administrator");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_default_role");

                    b.Property<bool>("IsManager")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_manager");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.Team", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.TeamAssignments", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long>("TaskId")
                        .HasColumnType("bigint")
                        .HasColumnName("fk_task");

                    b.Property<long>("TeamId")
                        .HasColumnType("bigint")
                        .HasColumnName("fk_team");

                    b.HasKey("Id");

                    b.ToTable("TeamAssignments");
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.Teammates", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<long>("TeamId")
                        .HasColumnType("bigint")
                        .HasColumnName("fk_team");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("fk_user");

                    b.HasKey("Id");

                    b.ToTable("Teammates");
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.Ticket", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.HasKey("Id");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("fullname");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("fk_role");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.UserAssignment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<DateTime>("AssignmentDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("assignment_date");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("comment");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("due_date");

                    b.Property<long>("TaskId")
                        .HasColumnType("bigint")
                        .HasColumnName("fk_task");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("fk_user");

                    b.HasKey("Id");

                    b.ToTable("UserAssignments");
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.UserRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.UserToken", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.RoleClaim", b =>
                {
                    b.HasOne("HalcyonFlowProject.Data.Database.Tables.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.UserClaim", b =>
                {
                    b.HasOne("HalcyonFlowProject.Data.Database.Tables.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.UserLogin", b =>
                {
                    b.HasOne("HalcyonFlowProject.Data.Database.Tables.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.UserRole", b =>
                {
                    b.HasOne("HalcyonFlowProject.Data.Database.Tables.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HalcyonFlowProject.Data.Database.Tables.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HalcyonFlowProject.Data.Database.Tables.UserToken", b =>
                {
                    b.HasOne("HalcyonFlowProject.Data.Database.Tables.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
