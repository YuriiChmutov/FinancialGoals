﻿// <auto-generated />
using System;
using FinancialGoals.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinancialGoals.Data.Migrations
{
    [DbContext(typeof(FinancialDbContext))]
    partial class FinancialDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CategoryFinancialAccount", b =>
                {
                    b.Property<int>("CategoriesCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("FinancialAccountsFinancialAccountId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesCategoryId", "FinancialAccountsFinancialAccountId");

                    b.HasIndex("FinancialAccountsFinancialAccountId");

                    b.ToTable("CategoryFinancialAccount");
                });

            modelBuilder.Entity("FinancialAccountGoal", b =>
                {
                    b.Property<int>("FinancialAccountsFinancialAccountId")
                        .HasColumnType("int");

                    b.Property<int>("GoalsGoalId")
                        .HasColumnType("int");

                    b.HasKey("FinancialAccountsFinancialAccountId", "GoalsGoalId");

                    b.HasIndex("GoalsGoalId");

                    b.ToTable("FinancialAccountGoal");
                });

            modelBuilder.Entity("FinancialGoals.Core.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Limit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("FinancialGoals.Core.Models.FinancialAccount", b =>
                {
                    b.Property<int>("FinancialAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FinancialAccountId"), 1L, 1);

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FinancialAccountId");

                    b.HasIndex("UserId");

                    b.ToTable("FinancialAccounts");
                });

            modelBuilder.Entity("FinancialGoals.Core.Models.Goal", b =>
                {
                    b.Property<int>("GoalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GoalId"), 1L, 1);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("TargetAmount")
                        .HasColumnType("float");

                    b.HasKey("GoalId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("FinancialGoals.Core.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"), 1L, 1);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FinancialAccountId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FinancialAccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("FinancialGoals.Core.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CategoryFinancialAccount", b =>
                {
                    b.HasOne("FinancialGoals.Core.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinancialGoals.Core.Models.FinancialAccount", null)
                        .WithMany()
                        .HasForeignKey("FinancialAccountsFinancialAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinancialAccountGoal", b =>
                {
                    b.HasOne("FinancialGoals.Core.Models.FinancialAccount", null)
                        .WithMany()
                        .HasForeignKey("FinancialAccountsFinancialAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinancialGoals.Core.Models.Goal", null)
                        .WithMany()
                        .HasForeignKey("GoalsGoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinancialGoals.Core.Models.FinancialAccount", b =>
                {
                    b.HasOne("FinancialGoals.Core.Models.User", "User")
                        .WithMany("FinancialAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinancialGoals.Core.Models.Transaction", b =>
                {
                    b.HasOne("FinancialGoals.Core.Models.Category", "Category")
                        .WithMany("Transactions")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinancialGoals.Core.Models.FinancialAccount", "FinancialAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("FinancialAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("FinancialAccount");
                });

            modelBuilder.Entity("FinancialGoals.Core.Models.Category", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("FinancialGoals.Core.Models.FinancialAccount", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("FinancialGoals.Core.Models.User", b =>
                {
                    b.Navigation("FinancialAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
