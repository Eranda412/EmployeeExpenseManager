using System;
using System.Collections.Generic;
using BaseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseAPI.FirstDbContext;

public partial class FirstDbContext : DbContext
{
    public FirstDbContext()
    {
    }

    public FirstDbContext(DbContextOptions<FirstDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:main-uthpala.database.windows.net,1433;Initial Catalog=my-free-sql-db-2400059;Persist Security Info=False;User ID=adminFor64Uthpala;Password=uthTestAdmin0059@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=90;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("User");

            entity.Property(e => e.Fname)
                .HasMaxLength(50)
                .HasColumnName("FName");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .HasColumnName("LName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
