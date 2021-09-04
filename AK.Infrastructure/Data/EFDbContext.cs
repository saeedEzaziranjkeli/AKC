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
        }
        public DbSet<User> Users { get; set; }
    }
}
