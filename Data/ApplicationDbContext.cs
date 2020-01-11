using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CascadeComboboxAsp2.Models;

namespace CascadeComboboxAsp2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<SubLocality> SubLocalities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



        }
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            

        }
    }
}
