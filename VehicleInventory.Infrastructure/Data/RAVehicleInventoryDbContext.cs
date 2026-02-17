using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace VehicleInventory.Infrastructure.Data;
public class RAVehicleInventoryDbContext : DbContext
{
    public RAVehicleInventoryDbContext(DbContextOptions<RAVehicleInventoryDbContext> options)
        : base(options) { }

    public DbSet<RAVehicleRow> Vehicles => Set<RAVehicleRow>();
    public DbSet<RAInventoryRow> Inventory => Set<RAInventoryRow>();
    public DbSet<RAVehicleTypeRow> VehicleTypes => Set<RAVehicleTypeRow>();
    public DbSet<RAVehicleStatusRow> VehicleStatuses => Set<RAVehicleStatusRow>();
    public DbSet<RAVehicleLocationRow> VehicleLocations => Set<RAVehicleLocationRow>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RAVehicleRow>().ToTable("Vehicle");
        modelBuilder.Entity<RAInventoryRow>().ToTable("Inventory");
        modelBuilder.Entity<RAVehicleTypeRow>().ToTable("VehicleType");
        modelBuilder.Entity<RAVehicleStatusRow>().ToTable("VehicleStatus");
        modelBuilder.Entity<RAVehicleLocationRow>().ToTable("VehicleLocation");
        modelBuilder.Entity<RAVehicleRow>().HasKey(x => x.Id);
        modelBuilder.Entity<RAInventoryRow>().HasKey(x => x.Id);
        modelBuilder.Entity<RAVehicleTypeRow>().HasKey(x => x.Id);
        modelBuilder.Entity<RAVehicleStatusRow>().HasKey(x => x.Id);
        modelBuilder.Entity<RAVehicleLocationRow>().HasKey(x => x.Id);

        modelBuilder.Entity<RAVehicleRow>()
            .HasOne(x => x.VehicleType)
            .WithMany()
            .HasForeignKey(x => x.VehicleTypeId);

        modelBuilder.Entity<RAInventoryRow>()
            .HasOne(x => x.Vehicle)
            .WithMany()
            .HasForeignKey(x => x.VehicleId);

        modelBuilder.Entity<RAInventoryRow>()
            .HasOne(x => x.VehicleLocation)
            .WithMany()
            .HasForeignKey(x => x.VehicleLocationId);

        modelBuilder.Entity<RAInventoryRow>()
            .HasOne(x => x.VehicleStatus)
            .WithMany()
            .HasForeignKey(x => x.VehicleStatusId);
    }
}
