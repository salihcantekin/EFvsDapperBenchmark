using ConsoleApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Persistence.EF.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<TeacherEF> Teachers { get; set; }
        public DbSet<StudentEF> Students { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public ApplicationDbContext()
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Constants.ConnectionStringEF);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("efdapperbenchmark");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("course");
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Id).HasColumnName("id");
                entity.Property(i => i.Name).HasColumnName("name").HasMaxLength(100);
                entity.Property(i => i.IsActive).HasColumnName("is_active").HasMaxLength(100);
            });

            modelBuilder.Entity<StudentEF>(entity =>
            {
                entity.ToTable("student");

                //entity.Ignore(i => i.Id);
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Id).HasColumnName("id");
                entity.Property(i => i.FirstName).HasColumnName("first_name");
                entity.Property(i => i.LastName).HasColumnName("last_name");
                entity.Property(i => i.BirthDate)
                        .HasColumnName("birth_date");
            });

            modelBuilder.Entity<TeacherEF>(entity =>
            {
                entity.ToTable("teacher");
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Id).HasColumnName("id");
                entity.Property(i => i.FirstName).HasColumnName("first_name").HasMaxLength(100);
                entity.Property(i => i.LastName).HasColumnName("last_name").HasMaxLength(100);
                entity.Property(i => i.BirthDate).HasColumnName("birth_date");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
