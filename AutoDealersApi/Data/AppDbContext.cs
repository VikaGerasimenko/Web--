using AutoDealersApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoDealersApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Dealer> Dealers => Set<Dealer>();
    public DbSet<Car> Cars => Set<Car>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dealer>(e =>
        {
            e.ToTable("dealers");
            e.Property(d => d.Id).HasColumnName("id");
            e.Property(d => d.Name).HasColumnName("name");
            e.Property(d => d.City).HasColumnName("city");
            e.Property(d => d.Address).HasColumnName("address");
            e.Property(d => d.Area).HasColumnName("area");
            e.Property(d => d.Rating).HasColumnName("rating");
        });

        modelBuilder.Entity<Car>(e =>
        {
            e.ToTable("cars");
            e.Property(c => c.Id).HasColumnName("id");
            e.Property(c => c.Firm).HasColumnName("firm");
            e.Property(c => c.Model).HasColumnName("model");
            e.Property(c => c.Year).HasColumnName("year");
            e.Property(c => c.Power).HasColumnName("power");
            e.Property(c => c.Color).HasColumnName("color");
            e.Property(c => c.Price).HasColumnName("price");
            e.Property(c => c.DealerId).HasColumnName("dealer_id");
        });
    }
}