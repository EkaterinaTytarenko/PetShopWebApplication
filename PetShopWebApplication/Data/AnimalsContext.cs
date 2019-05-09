using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetShopWebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace PetShopWebApplication.Data
{
    public class AnimalsContext:DbContext
    {
        public AnimalsContext(DbContextOptions<AnimalsContext> options) : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Cage> Cages { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<SpeciesFood> SpeciesFood { get; set; }
        public DbSet<Food> Food { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<Animal>().ToTable("Animal")
                    .HasOne(a=>a.Shop)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
                modelBuilder.Entity<Color>().ToTable("Color");
                modelBuilder.Entity<Shop>().ToTable("Shop");
                modelBuilder.Entity<Species>().ToTable("Species");
                modelBuilder.Entity<SpeciesFood>().ToTable("SpeciesFood");
                modelBuilder.Entity<Food>().ToTable("Food");
                modelBuilder.Entity<Cage>().ToTable("Cage");
            }
            catch(Exception e)
            { }
        }
    }
}
