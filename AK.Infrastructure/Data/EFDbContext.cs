using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using AK.Domain.Models;

namespace AK.Infrastructure.Data
{
    public class EFDbContext : DbContext
    {
        public  EFDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drug>(
                eb =>
                {
                    eb.Property(b => b.Code).HasMaxLength(30);
                    eb.Property(b => b.Price).HasColumnType("decimal(18, 2)");
                    eb.Property(b => b.Label).HasMaxLength(100);
                });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Drug> Drugs { get; set; }
    }
}
