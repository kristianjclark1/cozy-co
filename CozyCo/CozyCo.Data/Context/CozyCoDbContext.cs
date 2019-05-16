using CozyCo.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CozyCo.Data.Context
{
    // DbContext -> represent a session to a db APIs
    // to communicate with db
    public class CozyCoDbContext : IdentityDbContext<AppUser>
    {
        // Represents a collection (table) of a given entity/model
        // They map to tables by default
       public DbSet<Property> Properties { get; set; }
       public DbSet<PropertyType>PropertyTypes { get; set; }

        //Virtual medthod designed to be overridden
        // You can provide configuration information for the context
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connection string is divided in 3 elements
            //server - database - authentication 
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=cozyco;Trusted_Connection=true");
        }

        //We can manipulate the models
        //Add data to table
        //Change the default relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base = IdentityDbContext
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PropertyType>().HasData(
                new PropertyType { Id = 1, Description = "Condo"},
                new PropertyType { Id = 2, Description = "Single Family Home" },
                new PropertyType { Id = 3, Description = "Duplex" }
            );
        }
    }
}
