using System;
using System.Collections.Generic;
using AuthNAndAuthZ.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthNAndAuthZ.Data;

public partial class SecureDbContext : DbContext
{
    public SecureDbContext()
    {
    }

    public SecureDbContext(DbContextOptions<SecureDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RealUser> RealUsers { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

    //    => optionsBuilder.UseSqlServer("Data Source=(localdb)\\Mssqllocaldb;Initial Catalog=secureDb;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RealUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RealUser__3214EC070B9BB617");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(25);
            entity.Property(e => e.UserName).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
