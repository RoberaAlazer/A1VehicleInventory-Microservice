using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Robbiealazer8768364_Week3.Models;

namespace Robbiealazer8768364_Week3.Data;

public partial class CustomerProfileContext : DbContext
{
    public CustomerProfileContext()
    {
    }

    public CustomerProfileContext(DbContextOptions<CustomerProfileContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC070B76F2D9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}