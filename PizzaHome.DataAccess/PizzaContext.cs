using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PizzaHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.DataAccess
{
    public class PizzaContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = "Server=localhost; Database=PizzaHome; User Id=sa; Password=21821801";
            // connect to sql server with connection string from app settings
            options.UseSqlServer(connectionString);
        }

         protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Category>()
               .HasData(new Category { 
                Id = 1,
                Name = "Pizza 01",
                Description = "No description"

               });
         }

        public DbSet<Category>? Categories { get; set; }

        public DbSet<Product>? Product { get; set; }

        public DbSet<Shop>? Shops { get; set; }

       
    }
}
