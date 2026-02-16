using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VehicleInventory.Infrastructure.Data;

public class VehicleInventoryDbContext : DbContext
{
    public VehicleInventoryDbContext(DbContextOptions<VehicleInventoryDbContext> options)
        : base(options) { }

    public DbSet<VehicleRow> Vehicles => Set<VehicleRow>();
    public DbSet<InventoryRow> Inventory => Set<InventoryRow>();
    public DbSet<VehicleTypeRow> VehicleTypes => Set<VehicleTypeRow>();
    public DbSet<VehicleStatusRow> VehicleStatuses => Set<VehicleStatusRow>();
    public DbSet<VehicleLocationRow> VehicleLocations => Set<VehicleLocationRow>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VehicleRow>().ToTable("Vehicle");
        modelBuilder.Entity<InventoryRow>().ToTable("Inventory");
        modelBuilder.Entity<VehicleTypeRow>().ToTable("VehicleType");
        modelBuilder.Entity<VehicleStatusRow>().ToTable("VehicleStatus");
        modelBuilder.Entity<VehicleLocationRow>().ToTable("VehicleLocation");

        modelBuilder.Entity<VehicleRow>().HasKey(x => x.Id);
        modelBuilder.Entity<InventoryRow>().HasKey(x => x.Id);
        modelBuilder.Entity<VehicleTypeRow>().HasKey(x => x.Id);
        modelBuilder.Entity<VehicleStatusRow>().HasKey(x => x.Id);
        modelBuilder.Entity<VehicleLocationRow>().HasKey(x => x.Id);

        modelBuilder.Entity<VehicleRow>()
            .HasOne(x => x.VehicleType)
            .WithMany()
            .HasForeignKey(x => x.VehicleTypeId);

        modelBuilder.Entity<InventoryRow>()
            .HasOne(x => x.Vehicle)
            .WithMany()
            .HasForeignKey(x => x.VehicleId);

        modelBuilder.Entity<InventoryRow>()
            .HasOne(x => x.VehicleLocation)
            .WithMany()
            .HasForeignKey(x => x.VehicleLocationId);

        modelBuilder.Entity<InventoryRow>()
            .HasOne(x => x.VehicleStatus)
            .WithMany()
            .HasForeignKey(x => x.VehicleStatusId);
    }
}
