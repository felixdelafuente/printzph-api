using System;
using Microsoft.EntityFrameworkCore;
using PrintzPh.Domain.Entities;

namespace PrintzPh.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  public DbSet<User> Users { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<User>(entity =>
    {
      entity.HasKey(e => e.Id);

      entity.Property(e => e.FirstName)
              .IsRequired()
              .HasMaxLength(100);

      entity.Property(e => e.LastName)
              .IsRequired()
              .HasMaxLength(100);

      entity.Property(e => e.Email)
              .IsRequired()
              .HasMaxLength(255);

      entity.HasIndex(e => e.Email).IsUnique();

      entity.Property(e => e.PhoneNumber)
              .IsRequired()
              .HasMaxLength(20);

      entity.Property(e => e.Status)
              .IsRequired();

      entity.Property(e => e.CreatedAt)
              .IsRequired();

      entity.Property(e => e.UpdatedAt);
    });
  }
}
