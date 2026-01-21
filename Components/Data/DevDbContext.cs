using Microsoft.EntityFrameworkCore;
using dev_portfolio.Components.Models;

namespace dev_portfolio.Components.Data;

public class DevDbContext : DbContext
{
  public DevDbContext(DbContextOptions<DevDbContext> options) : base(options) { }

  public DbSet<Project> Projects { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<Project>(entity =>
    {
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Title).IsRequired();
    });
  }
}