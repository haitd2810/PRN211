using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;


namespace CRUD_Slot2.Models
{
    public partial class PRN221_1Context : DbContext
    {
        public static PRN221_1Context Ins = new PRN221_1Context();
        public PRN221_1Context()
        {
            if (Ins == null) Ins = this;
        }

        public PRN221_1Context(DbContextOptions<PRN221_1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            if (!optionsBuilder.IsConfigured) { optionsBuilder.UseSqlServer(config.GetConnectionString("value")); }
            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //                optionsBuilder.UseSqlServer("Data Source=PHANH\\PANH;Initial Catalog=PRN221_1; Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");
            //            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.DepartId)
                    .HasMaxLength(10)
                    .HasColumnName("departId");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Gpa).HasColumnName("gpa");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");

                entity.HasOne(d => d.Depart)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.DepartId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Department");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
